using Microsoft.AspNetCore.Identity;
using NuGet.Protocol;
using NuGet.Protocol.Plugins;
using sistema_reconocimiento.Interface;
using sistema_reconocimiento.Models;
using System.Security.Claims;
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
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                status.StatusCode = 0;
                status.Message = "User already exists";
                return status;
            }

            ApplicationUser user = new ApplicationUser
            {
                Name = model.Name,
                Email = model.Email,
                UserName = model.Username
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "User creation failed";
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
            status.Message = "User has been registered successfully!";
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
        }

    }
}
