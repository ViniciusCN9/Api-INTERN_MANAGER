using System.Collections.Generic;
using DesafioAPI.domain.Entities;

namespace DesafioAPI.domain.Repositories
{
    public interface IAccountRepository
    {
        List<Account> GetAccounts();

        Account GetByIdAccount(int id);

        Account PostLogin(string username, string password);

        void PostRegister(Account account);

        void UpdateAccount(Account account);

        void DeleteAccount(Account account);      
    }
}