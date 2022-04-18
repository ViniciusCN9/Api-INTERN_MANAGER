using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioAPI.application.DataTransferObjects
{
    public class AccountDto
    {
        [StringLength(25, ErrorMessage = "Nome de usuário deve ter no mínimo {2} e no máximo {1} caracter(es)", MinimumLength = 6)]
        public string Username { get; set; }

        [MinLength(6, ErrorMessage = "Senha deve ter no mínimo {1} caracter(es)")]
        public string Password { get; set; }

        public bool? IsActive { get; set; }
    }
}