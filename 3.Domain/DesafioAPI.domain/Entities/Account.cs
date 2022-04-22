using DesafioAPI.domain.Entities.Base;

namespace DesafioAPI.domain.Entities
{
    public class Account : Entity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
    }
}