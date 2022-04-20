using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAPI.application.DataTransferObjects;
using DesafioAPI.domain.Entities;

namespace DesafioAPI.application.Interfaces
{
    public interface ICategoryService
    {
        List<Category> GetCategories();

        Category GetByIdCategory(int id);

        Category PostCategory(CategoryCreateDto categoryDto);

        void PatchByIdCategory(CategoryUpdateDto categoryDto, int id);

        void PutByIdCategory(CategoryUpdateDto categoryDto, int id);

        void DeleteByIdCategory(int id);
    }
}