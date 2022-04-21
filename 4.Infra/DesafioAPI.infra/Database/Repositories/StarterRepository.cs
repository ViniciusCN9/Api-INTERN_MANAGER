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

        public List<Starter> GetStarters()
        {
            List<Starter> starters;
            try
            {
                starters = _context.Starters.Include(e => e.Category).AsNoTracking().ToList();
            }
            catch
            {
                starters = null;
            }

            return starters; 
        }

        public Starter GetByIdStarter(int Id)
        {
            Starter starter;
            try
            {
                starter = _context.Starters.Include(e => e.Category).FirstOrDefault(e => e.Id == Id);
            }
            catch
            {
                starter = null;
            }

            return starter;
        }

        public Starter GetByNameStarter(string name)
        {
            Starter starter;
            try
            {
                starter = _context.Starters.Include(e => e.Category).AsNoTracking().FirstOrDefault(e => e.Name == name);
            }
            catch
            {
                starter = null;
            }

            return starter;
        }

        public Starter GetLastStarter()
        {
            Starter starter;
            try
            {
                starter = _context.Starters.Include(e => e.Category).ToList().Last();
            }
            catch
            {
                starter = null;
            }

            return starter;
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