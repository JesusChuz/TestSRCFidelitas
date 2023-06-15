using sistema_reconocimiento.Models;
namespace sistema_reconocimiento.Interface
{
    public interface INotificationEmailService
    {
        Task<Status> SendNewPurchaseEmail(LoginModel login, Manager manager, Engineers engineers, Rewards reward, Purchases purchase);
    }
}
