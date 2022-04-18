using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAPI.domain.Entities;

namespace DesafioAPI.domain.Repositories
{
    public interface IAccountRepository
    {
        Task<List<Account>> GetAccounts();

        Task<Account> GetByIdAccount(int id);

        Task<Account> PostLogin(string username, string password);

        void PostRegister(Account account);

        void UpdateAccount();

        void DeleteByIdAccount(int id);      
    }
}