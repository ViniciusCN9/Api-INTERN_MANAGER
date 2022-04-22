using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAPI.domain.Entities;

namespace DesafioAPI.api.Hateoas.Containers
{
    public class StarterContainer
    {
        public Starter Starter { get; set; }
        public List<HateoasLink> Links { get; set; } = new List<HateoasLink>();
    }
}