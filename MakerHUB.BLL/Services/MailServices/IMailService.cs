namespace MakerHUB.BLL.Services.MailServices
{
    public interface IMailService
    {
        void Send(string message, string subject);
    }
}