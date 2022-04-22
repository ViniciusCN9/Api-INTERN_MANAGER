using System.Collections.Generic;
using System.Linq;
using DesafioAPI.domain.Entities;
using DesafioAPI.domain.Entities.Base;
using DesafioAPI.domain.Repositories;
using DesafioAPI.infra.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace DesafioAPI.infra.Database.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Account> GetAccounts()
        {
            List<Account> accounts;
            try
            {
                accounts = _context.Accounts.AsNoTracking().Where(e => e.Role == Role.USER).ToList();
            }
            catch
            {
                accounts = null;
            }

            return accounts;
        }

        public Account GetByIdAccount(int id)
        {
            Account account;
            try
            {
                account = _context.Accounts.Where(e => e.Role == Role.USER).First(e => e.Id == id);
            }
            catch
            {
                account = null;
            }
            
            return account;
        }

        public Account PostLogin(string username, string password)
        {
            Account account;
            try
            {
                account = _context.Accounts.AsNoTracking().First(e => e.Username == username && e.Password == password);
            }
            catch
            {
                account = null;
            }

            return account;
        }

        public void PostRegister(Account account)
        {
            _context.Accounts.Add(account);
            _context.SaveChanges();
        }

        public void UpdateAccount(Account account)
        {
            _context.Accounts.Update(account);
            _context.SaveChanges();
        }

        public void DeleteAccount(Account account)
        {
            _context.Accounts.Remove(account);
            _context.SaveChanges();
        }
    }
}