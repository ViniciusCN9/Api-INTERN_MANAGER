using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAPI.domain.Entities;

namespace DesafioAPI.domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategories();

        Task<Category> GetByIdCategory(int id);

        void PostCategory(Category category);

        void UpdateCategory();

        void DeleteByIdCategory(int id);
    }
}