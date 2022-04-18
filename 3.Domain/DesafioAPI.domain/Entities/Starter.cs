using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAPI.domain.Entities.Base;

namespace DesafioAPI.domain.Entities
{
    public class Starter : Entity
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Abbreviation { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
    }
}