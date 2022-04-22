using DesafioAPI.application.DataTransferObjects;
using DesafioAPI.domain.Entities;
using DesafioAPI.domain.Entities.Base;

namespace DesafioAPI.application.Mappers
{
    public static class AccountMapper
    {
        public static Account ToDomain(this AccountRegisterDto accountDto) => new Account
        {
            Username = accountDto.Username,
            Password = accountDto.Password,
            Email = accountDto.Email,
            Role = Role.USER,
            IsActive = true
        };
    }
}