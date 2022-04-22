namespace DesafioAPI.domain.Entities.Base
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
    }
}