using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using DesafioAPI.application.Interfaces;
using FluentEmail.Core;
using FluentEmail.Smtp;

namespace DesafioAPI.application.Services
{
    public class EmailService : IEmailService
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
            catch (Exception e)
            {
                throw new Exception("Falha ao enviar email" + e);
            }
            
        }
    }
}