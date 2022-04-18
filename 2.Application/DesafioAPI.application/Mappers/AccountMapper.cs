using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAPI.application.DataTransferObjects;
using DesafioAPI.domain.Entities;

namespace DesafioAPI.application.Mappers
{
    public static class AccountMapper
    {
        public static Account ToDomain(this AccountDto accountDto) => new Account
        {
            Username = accountDto.Username,
            Password = accountDto.Password,
            IsActive = true
        };
    }
}