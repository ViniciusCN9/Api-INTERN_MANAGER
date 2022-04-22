using System.Collections.Generic;
using DesafioAPI.domain.Entities;

namespace DesafioAPI.domain.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories();

        Category GetByIdCategory(int id);

        Category GetLastCategory();

        void PostCategory(Category category);

        void UpdateCategory(Category category);

        void DeleteCategory(Category category);
    }
}