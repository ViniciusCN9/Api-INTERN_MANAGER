using System.ComponentModel.DataAnnotations;

namespace DesafioAPI.application.DataTransferObjects
{
    public class AccountDto
    {
        [StringLength(25, ErrorMessage = "Nome de usuário deve ter no mínimo {2} e no máximo {1} caracter(es)", MinimumLength = 6)]
        public string Username { get; set; }

        [MinLength(6, ErrorMessage = "Senha deve ter no mínimo {1} caracter(es)")]
        public string Password { get; set; }

        [EmailAddress(ErrorMessage = "Informe um email válido")]
        public string Email { get; set; }

        public bool? IsActive { get; set; }
    }
}