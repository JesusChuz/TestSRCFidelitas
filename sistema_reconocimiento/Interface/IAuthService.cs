using sistema_reconocimiento.Models;
//se declara lo que se hace en la clase services
namespace sistema_reconocimiento.Interface
{
    public interface IAuthService
    {
        Task<Status> LoginAsync(LoginModel model);
        Task<Status> RegistrationAsync(AccountRegistration model);
        Task LogoutAsync();
        Task<Status> UpdatePasswordAsync(ApplicationUser email_check, LoginModel model);
        Task<Status> SendResetEmail(LoginModel model);
    }
}
