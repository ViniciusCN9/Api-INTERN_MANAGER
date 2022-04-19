using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAPI.domain.Entities;

namespace DesafioAPI.domain.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories();

        Category GetByIdCategory(int id);

        void PostCategory(Category category);

        void UpdateCategory(Category category);

        void DeleteCategory(Category category);
    }
}