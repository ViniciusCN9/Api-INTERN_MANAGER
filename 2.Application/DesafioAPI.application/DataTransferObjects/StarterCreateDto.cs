using System.ComponentModel.DataAnnotations;
using DesafioAPI.application.Validations;

namespace DesafioAPI.application.DataTransferObjects
{
    public class StarterCreateDto
    {
        [Required(ErrorMessage = "Informe um nome")]
        [StringLength(100, ErrorMessage = "Nome do starter deve ter no mínimo {2} e no máximo {1} caracter(es)", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Informe um cpf")]
        [Cpf]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Informe uma abreviação")]
        [Abbreviation]
        public string Abbreviation { get; set; }

        [Required(ErrorMessage = "Informe um email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a categoria")]
        public int CategoryId { get; set; }
    }
}