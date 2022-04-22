using System;
using System.Net.Mail;
using DesafioAPI.application.Interfaces;
using FluentEmail.Core;
using FluentEmail.Smtp;

namespace DesafioAPI.application.Services
{
    public class EmailLocalService : IEmailService
    {
        public void SendEmail(string email, string content)
        {
            try
            {
                var sender = new SmtpSender(() => new SmtpClient("localhost")
                {
                    EnableSsl = false,
                    DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                    PickupDirectoryLocation = @"C:\Starter\Email"
                });

                Email.DefaultSender = sender;

                var emailProperties = Email
                    .From("admin@teste.com")
                    .To(email)
                    .Subject("DesafioAPI - Email")
                    .Body(content)
                    .Send();
                }
            catch
            {
                throw new Exception("Falha ao enviar email");
            }
            
        }
    }
}