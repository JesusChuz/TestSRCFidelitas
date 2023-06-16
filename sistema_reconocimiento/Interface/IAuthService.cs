using sistema_reconocimiento.Models;
//se declara lo que se hace en la clase services
namespace sistema_reconocimiento.Interface
{
    public interface IAuthService
    {
        Task<Status> LoginAsync(LoginModel model);
        Task<Status> RegistrationAsync(AccountRegistration model);
        Task LogoutAsync();
        Task<Status> UpdatePasswordAsync(LoginModel model);
<<<<<<< HEAD
        Task<Status> SendResetEmail(LoginModel model);
=======
>>>>>>> 5214b57e3f10b832105456c72dacff5b1de60d2b
    }
}
