using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAPI.application.DataTransferObjects;
using DesafioAPI.domain.Entities;

namespace DesafioAPI.application.Interfaces
{
    public interface IAccountService
    {
        List<Account> GetAccounts();

        Account GetByIdAccount(int id);

        Account PostLogin(string username, string password);

        void PostRegister(AccountRegisterDto accountRegisterDto);

        void PatchByIdAccount(AccountDto accountDto, int id);

        void PutByIdAccount(AccountDto accountDto, int id);

        void DeleteByIdAccount(int id);
    }
}