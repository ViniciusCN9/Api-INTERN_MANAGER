using System.ComponentModel.DataAnnotations;

namespace DesafioAPI.application.DataTransferObjects
{
    public class CategoryUpdateDto
    {
        [StringLength(25, ErrorMessage = "Nome da categoria deve ter no mínimo {2} e no máximo {1} caracter(es)", MinimumLength = 2)]
        public string Name { get; set; }

        [StringLength(25, ErrorMessage = "Tecnologia deve ter no mínimo {2} e no máximo {1} caracter(es)", MinimumLength = 2)]
        public string Technology { get; set; }

        public bool? IsActive { get; set; }
    }
}