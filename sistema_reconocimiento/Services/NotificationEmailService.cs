using Microsoft.AspNetCore.Identity;
using sistema_reconocimiento.Interface;
using sistema_reconocimiento.Models;
using MimeKit;
using MailKit.Net.Smtp;
using MessagePack;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.Net.Mail;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authentication;
using System.Configuration;


namespace sistema_reconocimiento.Services
{
    public class NotificationEmailService : INotificationEmailService
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        private readonly string _varConnStr;
        public NotificationEmailService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            _varConnStr = _configuration.GetValue<string>("ConnectionStrings:DBConnectionString");
        }
        private static string GetHtmlTemplate(string templateFileName)
        {
            // Leer el contenido del archivo de plantilla HTML desde el disco o cualquier otra fuente de datos
            string templatePath = Path.Combine("Views", "MailTemplates", templateFileName);
            string htmlTemplate = File.ReadAllText(templatePath);
            return htmlTemplate;
        }
        public void GetMyManager(Purchases purchase, Manager manager, LoginModel login, Engineers engineer, Rewards reward)
        {
            using (SqlConnection connection = new SqlConnection(_varConnStr))
            {
                try
                {
                    connection.Open();
                    // Crear el comando y asignar el stored procedure
                    using (SqlCommand command = new SqlCommand("SelectMyManager", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        // Agregar parámetro de entrada
                        command.Parameters.AddWithValue("@ID_Engineer", purchase.Engineer_ID);
                        command.Parameters.AddWithValue("@Reward_ID", purchase.Reward_ID);
                        command.Parameters.AddWithValue("@ID_Purchase", purchase.ID_Purchase);

                        // Ejecutar el stored procedure y leer los resultados
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                manager.Email = (string)reader["Email"];
                                manager.Name_Manager = (string)reader["Name_Manager"];
                                login.Email = (string)reader["EmailE"];
                                engineer.Name_Engineer = (string)reader["Name_Engineer"];
                                engineer.Points = (int)reader["Points"];
                                reward.Reward_Name = (string)reader["Reward_Name"];
                                reward.Price = (int)reader["Price"];
                            }
                            connection.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    connection.Close();
                }
            }
        }
        public async Task<Status> SendNewPurchaseEmail(LoginModel login, Manager manager, Engineers engineer, Rewards reward, Purchases purchase)
        {
            var status = new Status();
            try
            {
                //primero valida si el correo existe
                var email_notification = new MimeMessage();
                string email_from = "src.notificaciones@gmail.com";
                GetMyManager(purchase, manager, login, engineer, reward);
                string[] destinatarios = { login.Email, manager.Email};

                foreach (var destinatario in destinatarios)
                {
                    email_notification.To.Add(MailboxAddress.Parse(destinatario));
                }

                email_notification.From.Add(MailboxAddress.Parse(email_from));
//              email_notification.To.Add(MailboxAddress.Parse(email_to));
                email_notification.Subject = "New Purchase";

                // Cargar la plantilla HTML
                string htmlBody = GetHtmlTemplate("NewPurchase.html");
                string precio = purchase.Reward_Price.ToString();
                string points = engineer.Points.ToString();

                // Reemplazar los marcadores de posición en la plantilla con los valores específicos
                htmlBody = htmlBody.Replace("[NameEngineer]", engineer.Name_Engineer);
                htmlBody = htmlBody.Replace("[NameManager]", manager.Name_Manager);
                htmlBody = htmlBody.Replace("[NombreRecompensa]", reward.Reward_Name);
                htmlBody = htmlBody.Replace("[PrecioRecompensa]", precio);
                htmlBody = htmlBody.Replace("[PuntosRestantes]", points);

                // Crear el contenido HTML del mensaje
                var body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = htmlBody
                };

                // Adjuntar el contenido al mensaje
                email_notification.Body = body;

                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate(email_from, "wkwablffrzoeigwz");
                smtp.Send(email_notification);
                smtp.Disconnect(true);

                status.StatusCode = 1;
                status.Message = "email sent!";

                return status;
            }
            catch
            {
                status.StatusCode = 0;
                status.Message = "failed to send email, an error happened";
                return status;
            }
        }
    }
}
