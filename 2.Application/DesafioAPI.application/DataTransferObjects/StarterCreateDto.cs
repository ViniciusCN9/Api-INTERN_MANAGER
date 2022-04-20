using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioAPI.application.DataTransferObjects
{
    public class StarterCreateDto
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Abbreviation { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public int CategoryId { get; set; }
    }
}