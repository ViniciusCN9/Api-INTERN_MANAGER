using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioAPI.domain.Entities.Base
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
    }
}