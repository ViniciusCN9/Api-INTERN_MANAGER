using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioAPI.application.DataTransferObjects
{
    public class CategoryDto
    {
        [StringLength(25, ErrorMessage = "Nome da categoria deve ter no mínimo {2} e no máximo {1} caracter(es)", MinimumLength = 4)]
        public string Name { get; set; }

        [StringLength(25, ErrorMessage = "Tecnologia deve ter no mínimo {2} e no máximo {1} caracter(es)", MinimumLength = 4)]
        public string Technology { get; set; }

        public bool? IsActive { get; set; }
    }
}