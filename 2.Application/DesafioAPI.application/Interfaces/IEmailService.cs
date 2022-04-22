namespace DesafioAPI.application.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(string email, string content);
    }
}