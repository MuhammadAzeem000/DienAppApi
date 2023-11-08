using DienappApi.Models;

namespace DienappApi.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(Message message);
    }
}