using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DesafioAPI.application.Validations;
using Microsoft.AspNetCore.Http;

namespace DesafioAPI.application.DataTransferObjects
{
    public class StarterUpdateDto
    {
        [StringLength(100, ErrorMessage = "Nome do starter deve ter no mínimo {2} e no máximo {1} caracter(es)", MinimumLength = 2)]
        public string Name { get; set; }

        [Cpf]
        public string Cpf { get; set; }

        [Abbreviation]
        public string Abbreviation { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public int CategoryId { get; set; }
        public bool? IsActive { get; set; }
    }
}