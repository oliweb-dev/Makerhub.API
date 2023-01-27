using System.Net;
using System.Net.Mail;

namespace MakerHUB.BLL.Services.MailServices
{
    public class MailService : IMailService
    {
        public void Send(string message, string subject)
        {
            MailAddress senderEmail = new MailAddress("email", "name");
            MailAddress receiverEmail = new MailAddress("email", "name");
            string password = "...";
            string sub = subject;
            string body = message;
            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address, password)
            };
            using (MailMessage mess = new MailMessage(senderEmail, receiverEmail)
            {
                Subject = sub,
                Body = body
                //IsBodyHtml = true
            })
            {
                smtp.Send(mess);
            }
        }
    }
}
