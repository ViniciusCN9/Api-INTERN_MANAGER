using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioAPI.application.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(string email, string content);
    }
}