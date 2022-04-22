using DesafioAPI.domain.Entities;

namespace DesafioAPI.application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(Account account);
    }
}