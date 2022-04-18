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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<List<Category>> GetCategories()
        {
            return _context.Categories.AsNoTracking().ToListAsync();
        }

        public Task<Category> GetByIdCategory(int id)
        {
            return _context.Categories.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public void PostCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void UpdateCategory()
        {
            _context.SaveChanges();
        }

        public void DeleteByIdCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(e => e.Id == id);
            _context.Categories.Remove(category);
        }
    }
}