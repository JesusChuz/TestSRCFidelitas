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
            string templatePath = Path.Combine("wwwroot", "MailTemplates", templateFileName);
            string htmlTemplate = File.ReadAllText(templatePath);
            return htmlTemplate;
        }
        public void GetMyManagerForPurchase(Purchases purchase, Manager manager, LoginModel login, Engineers engineer, Rewards reward)
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
                GetMyManagerForPurchase(purchase, manager, login, engineer, reward);
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
        public void GetEngineerEmail(LoginModel login, Engineers engineer, Recognitions recognitions)
        {
            using (SqlConnection connection = new SqlConnection(_varConnStr))
            {
                try
                {
                    connection.Open();
                    // Crear el comando y asignar el stored procedure
                    using (SqlCommand command = new SqlCommand("GetEngineerEmail", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        // Agregar parámetro de entrada
                        command.Parameters.AddWithValue("@ID_Engineer", recognitions.Petitioner_Eng);

                        // Ejecutar el stored procedure y leer los resultados
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                recognitions.Petitioner_EngEmail = (string)reader["Email"];
                                recognitions.Petitioner_EngFullName = (string)reader["FullName"];
                            }
                            connection.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void GetManagerForRecognition(LoginModel login, Engineers engineer, Recognitions recogniton, Manager manager)
        {
            using (SqlConnection connection = new SqlConnection(_varConnStr))
            {
                try
                {
                    connection.Open();
                    // Crear el comando y asignar el stored procedure
                    using (SqlCommand command = new SqlCommand("SelectManagerforRecognition", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        // Agregar parámetro de entrada
                        command.Parameters.AddWithValue("@ID_Engineer", recogniton.Recognized_Eng);
                        command.Parameters.AddWithValue("@ID_Recognition", recogniton.ID_Recognition);


                        // Ejecutar el stored procedure y leer los resultados
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                manager.Email = (string)reader["Email"];
                                manager.Name_Manager = (string)reader["Name_Manager"];
                                login.Email = (string)reader["EmailE"];
                                engineer.Name_Engineer = (string)reader["Name_Engineer"];
                               // recogniton.ID_Recognition = (int)reader["ID_Recognition"];
                                recogniton.Case_Number = (string)reader["Case_Number"];
                                recogniton.Comment = (string)reader["Comment"];
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

        public async Task<Status> SendNewRecognition(LoginModel login, Engineers engineer, Recognitions recogniton, Manager manager)
        {
            var status = new Status();
            try
            {
                //primero valida si el correo existe
                var email_notification = new MimeMessage();
                string email_from = "src.notificaciones@gmail.com";
                GetManagerForRecognition(login, engineer, recogniton, manager);
                //GetEngineerEmail(login, engineer);
                GetEngineerEmail(login, engineer, recogniton);
                
                string[] destinatarios = { recogniton.Petitioner_EngEmail, login.Email, manager.Email};

                foreach (var destinatario in destinatarios)
                {
                    email_notification.To.Add(MailboxAddress.Parse(destinatario));
                }

                email_notification.From.Add(MailboxAddress.Parse(email_from));
                email_notification.Subject = "New Recognition";

                // Cargar la plantilla HTML
                string htmlBody = GetHtmlTemplate("NewRecognition.html");

                // Reemplazar los marcadores de posición en la plantilla con los valores específicos
                htmlBody = htmlBody.Replace("[NameEngineerRecognized]", engineer.Name_Engineer);
                htmlBody = htmlBody.Replace("[NameManager]", manager.Name_Manager);
                htmlBody = htmlBody.Replace("[RecognitionComments]", recogniton.Comment);
                htmlBody = htmlBody.Replace("[RecognitionCaseNumber]", recogniton.Case_Number);
                htmlBody = htmlBody.Replace("[NameEngineerPetitioner]", recogniton.Petitioner_EngFullName);

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
        public async Task<Status> SendStateRecognition(LoginModel login, Engineers engineer, SubmitStateRecognition recogniton, Manager manager)
        {
            var status = new Status();
            try
            {
                //primero valida si el correo existe
                var email_notification = new MimeMessage();
                string email_from = "src.notificaciones@gmail.com";
                GetManagerForRecognition(login, engineer, recogniton.RecognitionsPOST, manager);
                //GetEngineerEmail(login, engineer);
                GetEngineerEmail(login, engineer, recogniton.RecognitionsPOST);
                recogniton.RecognitionsPOST.Recognized_EngEmail = login.Email;
                string[] destinatarios = { recogniton.RecognitionsPOST.Petitioner_EngEmail, login.Email, manager.Email };

                foreach (var destinatario in destinatarios)
                {
                    email_notification.To.Add(MailboxAddress.Parse(destinatario));
                }
                string Recognition_ID = recogniton.RecognitionsPOST.ID_Recognition.ToString();
                email_notification.From.Add(MailboxAddress.Parse(email_from));
                email_notification.Subject = "The recognition (" + Recognition_ID + ") has been reviewed";

                // Cargar la plantilla HTML
                string htmlBody = GetHtmlTemplate("RecognitionState.html");

                // Reemplazar los marcadores de posición en la plantilla con los valores específicos
                htmlBody = htmlBody.Replace("[ID_Recognition]", recogniton.RecognitionsPOST.ID_Recognition.ToString());
                htmlBody = htmlBody.Replace("[NameEngineerRecognized]", engineer.Name_Engineer);
                htmlBody = htmlBody.Replace("[NameManager]", manager.Name_Manager);
                htmlBody = htmlBody.Replace("[RecognitionComments]", recogniton.RecognitionsPOST.Comment);
                htmlBody = htmlBody.Replace("[RecognitionCaseNumber]", recogniton.RecognitionsPOST.Case_Number);
                htmlBody = htmlBody.Replace("[NameEngineerPetitioner]", recogniton.RecognitionsPOST.Petitioner_EngFullName);
                htmlBody = htmlBody.Replace("[RecognitionState]", recogniton.RecognitionsPOST.Recognition_State);

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
