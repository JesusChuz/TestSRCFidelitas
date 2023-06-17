using sistema_reconocimiento.Models;

namespace sistema_reconocimiento.Interface
{
    public interface INotificationEmailService
    {
        Task<Status> SendNewPurchaseEmail(LoginModel login, Manager manager, Engineers engineers, Rewards reward, Purchases purchase);
        Task<Status> SendNewRecognition(LoginModel login, Engineers engineer, Recognitions recogniton, Manager manager);
        Task<Status> SendStateRecognition(LoginModel login, Engineers engineer, SubmitStateRecognition recogniton, Manager manager);
    }
}
