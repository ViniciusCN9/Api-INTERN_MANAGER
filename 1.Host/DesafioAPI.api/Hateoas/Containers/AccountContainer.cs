using System.Collections.Generic;
using DesafioAPI.domain.Entities;

namespace DesafioAPI.api.Hateoas.Containers
{
    public class AccountContainer
    {
        public Account Account { get; set; }
        public List<HateoasLink> Links { get; set; } = new List<HateoasLink>();
         
    }
}