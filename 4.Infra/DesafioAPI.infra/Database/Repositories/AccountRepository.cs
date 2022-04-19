using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAPI.domain.Entities;
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

        public Task<List<Account>> GetAccounts()
        {
            return _context.Accounts.AsNoTracking().ToListAsync();
        }

        public Task<Account> GetByIdAccount(int id)
        {
            return _context.Accounts.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public Task<Account> PostLogin(string username, string password)
        {
            return _context.Accounts.AsNoTracking().FirstOrDefaultAsync(e => e.Username == username && e.Password == password);
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