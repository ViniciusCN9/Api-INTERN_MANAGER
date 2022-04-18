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
        Task<List<Category>> GetCategories();

        Task<Category> GetByIdCategory(int id);

        void PostCategory(CategoryDto categoryDto);

        void PatchByIdCategory(CategoryDto categoryDto, int id);

        void PutByIdCategory(CategoryDto categoryDto, int id);

        void DeleteByIdCategory(int id);
    }
}