using System.ComponentModel.DataAnnotations;

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