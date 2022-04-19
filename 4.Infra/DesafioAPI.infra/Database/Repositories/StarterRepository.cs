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
    public class StarterRepository : IStarterRepository
    {
        private readonly ApplicationDbContext _context;

        public StarterRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<List<Starter>> GetStarters()
        {
            return _context.Starters.Include(e => e.Category).AsNoTracking().ToListAsync();
        }

        public Task<Starter> GetByIdStarter(int Id)
        {
            return _context.Starters.Include(e => e.Category).AsNoTracking().FirstOrDefaultAsync(e => e.Id == Id);
        }

        public Task<Starter> GetByNameStarter(string name)
        {
            return _context.Starters.Include(e => e.Category).AsNoTracking().FirstOrDefaultAsync(e => e.Name == name);
        }

        public void PostStarter(Starter starter)
        {
            _context.Starters.Add(starter);
            _context.SaveChanges();
        }

        public void UpdateStarter(Starter starter)
        {
            _context.Starters.Update(starter);
            _context.SaveChanges();
        }

        public void DeleteStarter(Starter starter)
        {
            _context.Starters.Remove(starter);
            _context.SaveChanges();
        }
    }
}