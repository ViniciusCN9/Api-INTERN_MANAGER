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

        public List<Category> GetCategories()
        {
            List<Category> categories;
            try
            {
                categories = _context.Categories.AsNoTracking().ToList();
            }
            catch
            {
                categories = null;
            }

            return categories;
        }

        public Category GetByIdCategory(int id)
        {
            Category category;
            try
            {
                category = _context.Categories.AsNoTracking().FirstOrDefault(e => e.Id == id);
            }
            catch
            {
                category = null;
            }

            return category;
        }

        public void PostCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }

        public void DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}