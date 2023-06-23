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
>>>>>>> 996bfd33ae1f3c389c05516131f52ce72c62ba76
    }
}
