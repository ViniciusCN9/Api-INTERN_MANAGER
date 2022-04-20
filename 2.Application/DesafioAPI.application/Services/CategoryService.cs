using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAPI.application.DataTransferObjects;
using DesafioAPI.application.Interfaces;
using DesafioAPI.application.Mappers;
using DesafioAPI.domain.Entities;
using DesafioAPI.domain.Repositories;

namespace DesafioAPI.application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public List<Category> GetCategories()
        {
            var categories = _categoryRepository.GetCategories();
            if (!categories.Any())
                throw new ArgumentException("Nenhuma categoria encontrada");

            return categories;
        }

        public Category GetByIdCategory(int id)
        {
            if (id < 0)
                throw new ArgumentException("Id inválido");

            var category = _categoryRepository.GetByIdCategory(id);
            if (category is null)
                throw new ArgumentException("Categoria não encontrada");

            return category; 
        }

        public Category PostCategory(CategoryCreateDto categoryDto)
        {
            _categoryRepository.PostCategory(categoryDto.ToDomain());
            return _categoryRepository.GetLastCategory();
        }

        public void PatchByIdCategory(CategoryUpdateDto categoryDto, int id)
        {
            if (id < 0)
                throw new ArgumentException("Id inválido");

            var category = _categoryRepository.GetByIdCategory(id);
            if (category is null)
                throw new ArgumentException("Categoria não encontrada");

            category.Name = categoryDto.Name ?? category.Name;
            category.Technology = categoryDto.Technology ?? category.Technology;
            category.IsActive = categoryDto.IsActive ?? category.IsActive;

            _categoryRepository.UpdateCategory(category);
        }

        public void PutByIdCategory(CategoryUpdateDto categoryDto, int id)
        {
            if (id < 0)
                throw new ArgumentException("Id inválido");
                
            var category = _categoryRepository.GetByIdCategory(id);
            if (category is null)
                throw new ArgumentException("Categoria não encontrada");

            category.Name = categoryDto.Name;
            category.Technology = categoryDto.Technology;
            category.IsActive = (bool)categoryDto.IsActive;

            _categoryRepository.UpdateCategory(category);
        }

        public void DeleteByIdCategory(int id)
        {
            if (id < 0)
                throw new ArgumentException("Id inválido");
                
            var category = _categoryRepository.GetByIdCategory(id);
            if (category is null)
                throw new ArgumentException("Categoria não encontrada");

            _categoryRepository.DeleteCategory(category);
        }
    }
}