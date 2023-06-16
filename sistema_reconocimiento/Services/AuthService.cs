using Microsoft.AspNetCore.Identity;
using NuGet.Protocol;
using NuGet.Protocol.Plugins;
using sistema_reconocimiento.Interface;
using sistema_reconocimiento.Models;
using System.Security.Claims;
<<<<<<< HEAD
using MimeKit;
using MailKit.Net.Smtp;
using MessagePack;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.Net.Mail;
using System.Text;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

=======
>>>>>>> 5214b57e3f10b832105456c72dacff5b1de60d2b
//Se programa toda la logica de los metodos que posteriormente se vuelven a llamar en la interface
namespace sistema_reconocimiento.Services
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public AuthService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public async Task<Status> LoginAsync(LoginModel model)
        {
            var status = new Status();
            var user = await userManager.FindByEmailAsync(model.Email);
            //primero valida si el correo existe
            if (user == null)
            {
                status.StatusCode = 0;
                status.Message = "Invalid email";
                return status;
            }
            //valida si la contraseña es correcta, por detras desencripta hash 
            if (!await userManager.CheckPasswordAsync(user, model.Password))
            {
                status.StatusCode = 0;
                status.Message = "Invalid password";
                return status;
            }
            var signInResult = await signInManager.PasswordSignInAsync(user, model.Password, false, true);
            if (signInResult.Succeeded)
            {
                //consulta el rol del usuario que se acaba de autenticar
                var userRoles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                status.StatusCode = 1;
                status.Message = "Logged in succesfully";
                return status;
            }
            else if (signInResult.IsLockedOut)
            {
                status.StatusCode = 0;
                status.Message = "user locked out";
                return status;
            }
            else
            {
                status.StatusCode = 0;
                status.Message = "Error on loggin in";
                return status;
            }
        }

        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<Status> RegistrationAsync(AccountRegistration model)
        {
            var status = new Status();
<<<<<<< HEAD
            var userExists = await userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
            {
                status.StatusCode = 0;
                status.Message = "Account already exists";
=======
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                status.StatusCode = 0;
                status.Message = "User already exists";
>>>>>>> 5214b57e3f10b832105456c72dacff5b1de60d2b
                return status;
            }

            ApplicationUser user = new ApplicationUser
            {
                Name = model.Name,
                Email = model.Email,
                UserName = model.Username
            };
<<<<<<< HEAD
            bool setIsNew = true;
            user.IsNew = setIsNew;
=======
>>>>>>> 5214b57e3f10b832105456c72dacff5b1de60d2b

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                status.StatusCode = 0;
<<<<<<< HEAD
                status.Message = "Account creation failed";
=======
                status.Message = "User creation failed";
>>>>>>> 5214b57e3f10b832105456c72dacff5b1de60d2b
                return status;
            }
            // role management 
            if (!await roleManager.RoleExistsAsync(model.Role))
                await roleManager.CreateAsync(new IdentityRole(model.Role));

            if (await roleManager.RoleExistsAsync(model.Role))
            {
                await userManager.AddToRoleAsync(user, model.Role);
            }
            status.StatusCode = 1;
<<<<<<< HEAD
            status.Message = "Account has been registered successfully!";
=======
            status.Message = "User has been registered successfully!";
>>>>>>> 5214b57e3f10b832105456c72dacff5b1de60d2b
            return status;
        }

        /*  public async Task<Status> UpdateAsync(LoginModel model)
          {
              var status = new Status();
              var user = await userManager.FindByEmailAsync(model.Email);

              var token = await userManager.GeneratePasswordResetTokenAsync(user);

              var result = await userManager.ResetPasswordAsync(user, token, model.Password);
              if (!result.Succeeded)
              {
                  status.StatusCode = 0;
                  status.Message = "Password update failed";
                  return status;
              }
              status.StatusCode = 1;
              status.Message = "Password has been updated successfully!";
              return status;
          }*/
        public async Task<Status> UpdatePasswordAsync(LoginModel model)
        {
            var status = new Status();
            var email_check = await userManager.FindByEmailAsync(model.Email);
            //primero valida si el correo existe
            if (email_check == null)
            {
                status.StatusCode = 0;
                status.Message = "Invalid email";
                return status;
            }
            //valida si la contraseña es correcta, por detras desencripta hash 
            if (!await userManager.CheckPasswordAsync(email_check, model.OldPassword))
            {
                status.StatusCode = 0;
                status.Message = "Invalid password";
                return status;
            }
            var signInResult = await signInManager.PasswordSignInAsync(email_check, model.OldPassword, false, true);
            if (signInResult.Succeeded)
            {
                //consulta el rol del usuario que se acaba de autenticar
                var userRoles = await userManager.GetRolesAsync(email_check);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, email_check.UserName)
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                status.StatusCode = 1;
                if (model.Password == model.ConfirmPassword)
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(email_check);
                    var result = await userManager.ResetPasswordAsync(email_check, token, model.Password);
                    if (!result.Succeeded)
                    {
                        status.StatusCode = 0;
                        status.Message = "Password update failed";
                        return status;
                    }
                    status.StatusCode = 1;
                    status.Message = "Password has been updated successfully!";
                    return status;
                }
                else
                {
                    status.StatusCode = 0;
                    status.Message = "Passwords does not match";
                    return status;
                }
            }
            else if (signInResult.IsLockedOut)
            {
                status.StatusCode = 0;
                status.Message = "user locked out";
                return status;
            }
            else
            {
                status.StatusCode = 0;
                status.Message = "Error on loggin in";
                return status;
            }
        }
<<<<<<< HEAD

        public static string GenerateNewPassword()
        {
            Random random = new Random();
            string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()";
            int length = 10;
            StringBuilder sb = new StringBuilder(length);

            bool hasDigit = false;
            bool hasUppercase = false;
            bool hasLowercase = false;
            bool hasPunctuation = false;

            while (sb.Length < length || !hasDigit || !hasUppercase || !hasLowercase || !hasPunctuation)
            {
                int randomIndex = random.Next(characters.Length);
                char randomChar = characters[randomIndex];
                sb.Append(randomChar);

                hasDigit = hasDigit || Char.IsDigit(randomChar);
                hasUppercase = hasUppercase || Char.IsUpper(randomChar);
                hasLowercase = hasLowercase || Char.IsLower(randomChar);
                hasPunctuation = hasPunctuation || IsPunctuation(randomChar);
            }
            return sb.ToString();   
        }
        private static bool IsPunctuation(char c)
        {
            return char.IsPunctuation(c) || char.IsSymbol(c);
        }

        private static string GetHtmlTemplate(string templateFileName)
        {
            // Leer el contenido del archivo de plantilla HTML desde el disco o cualquier otra fuente de datos
            string templatePath = Path.Combine("Views", "MailTemplates", templateFileName);
            string htmlTemplate = File.ReadAllText(templatePath);
            return htmlTemplate;
        }
        public async Task<Status> SendResetEmail(LoginModel model)
        {
            var status = new Status();
            try {
                //primero valida si el correo existe
                var email_notification = new MimeMessage();
                string email_from = "src.notificaciones@gmail.com";
                string email_to = model.Email;
                string newResettedPasword = GenerateNewPassword();

                model.Password = newResettedPasword;
                var email_check = await userManager.FindByEmailAsync(model.Email);
                var token = await userManager.GeneratePasswordResetTokenAsync(email_check);
                var result = await userManager.ResetPasswordAsync(email_check, token, model.Password);

                email_notification.From.Add(MailboxAddress.Parse(email_from));
                email_notification.To.Add(MailboxAddress.Parse(email_to));
                email_notification.Subject = "Password reset request";

                // Cargar la plantilla HTML
                string htmlBody = GetHtmlTemplate("NewPasswordReset.html");

                // Reemplazar los marcadores de posición en la plantilla con los valores específicos
                htmlBody = htmlBody.Replace("[NewPassword]", newResettedPasword);

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
=======
>>>>>>> 5214b57e3f10b832105456c72dacff5b1de60d2b
    }
}
