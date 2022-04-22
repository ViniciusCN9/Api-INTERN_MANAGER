using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using DesafioAPI.application.Interfaces;

namespace DesafioAPI.application.Services
{
    public class EmailGmailService : IEmailService
    {
        public void SendEmail(string email, string content)
        {
            try
            {
                MailMessage mailMessage = new MailMessage("vinicius.desafioapi@gmail.com", email);

                mailMessage.Subject = "DesafioAPI - Email sender";
                mailMessage.Body = content;
                mailMessage.SubjectEncoding = Encoding.GetEncoding("UTF-8");
                mailMessage.BodyEncoding = Encoding.GetEncoding("UTF-8");

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("vinicius.desafioapi@gmail.com", "desafioapi123");

                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMessage);
            }
            catch
            {
                throw new Exception("Falha ao enviar email");
            }
        }
    }
}