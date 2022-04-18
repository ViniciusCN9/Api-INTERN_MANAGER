using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAPI.domain.Entities.Base;

namespace DesafioAPI.domain.Entities
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public string Technology { get; set; }
    }
}