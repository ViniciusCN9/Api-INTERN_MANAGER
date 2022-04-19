using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioAPI.application.DataTransferObjects
{
    public class AccountLoginDto
    {
        [Required(ErrorMessage = "Informe o nome do usuário")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        public string Password { get; set; }
    }
}