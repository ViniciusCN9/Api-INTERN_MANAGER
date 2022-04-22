using System.ComponentModel.DataAnnotations;

namespace DesafioAPI.application.DataTransferObjects
{
    public class CategoryCreateDto
    {
        [Required(ErrorMessage = "Informe um nome")]
        [StringLength(25, ErrorMessage = "Nome da categoria deve ter no mínimo {2} e no máximo {1} caracter(es)", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Informe uma tecnologia")]
        [StringLength(25, ErrorMessage = "Tecnologia deve ter no mínimo {2} e no máximo {1} caracter(es)", MinimumLength = 2)]
        public string Technology { get; set; }
    }
}