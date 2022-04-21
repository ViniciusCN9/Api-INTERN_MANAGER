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
    public class StarterService : IStarterService
    {
        private readonly IStarterRepository _starterRepository;
        private readonly ICategoryRepository _categoryRepository;

        public StarterService(IStarterRepository starterRepository, ICategoryRepository categoryRepository)
        {
            _starterRepository = starterRepository;
            _categoryRepository = categoryRepository;
        }

        public List<Starter> GetStarters()
        {
            var starters = _starterRepository.GetStarters();
            if (!starters.Any())
                throw new ArgumentException("Nenhum starter encontrado");

            return starters;
        }

        public Starter GetByIdStarter(int id)
        {
            if (id < 0)
                throw new ArgumentException("Id inválido");

            var starter = _starterRepository.GetByIdStarter(id);
            if (starter is null)
                throw new ArgumentException("Starter não encontrado");

            return starter;
        }

        public Starter GetByNameStarter(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Nome inválido");

            var starter = _starterRepository.GetByNameStarter(name);
            if (starter is null)
                throw new ArgumentException($"Starter {name} não encontrado");

            return starter;
        }

        public Starter PostStarter(StarterCreateDto starterDto)
        {
            if (_categoryRepository.GetCategories() is null)
                throw new ArgumentException("Nenhuma categoria cadastrada");

            var category = _categoryRepository.GetByIdCategory(starterDto.CategoryId);
            if (category is null)
                throw new ArgumentException("Categoria não encontrada");

            var starterMapped = starterDto.ToDomain();
            starterMapped.Category = category;

            _starterRepository.PostStarter(starterMapped);
            var starter = _starterRepository.GetLastStarter();
            return starter;
        }

        public void UploadPhotoByIdStarter(Starter starter, string path)
        {
            Starter starterWithPhoto = starter;
            starterWithPhoto.Photo = path;

            _starterRepository.UpdateStarter(starterWithPhoto);
        }

        public void PatchByIdStarter(StarterUpdateDto starterDto, int id)
        {
            if (id < 0)
                throw new ArgumentException("Id inválido");

            var starter = _starterRepository.GetByIdStarter(id);
            if (starter is null)
                throw new ArgumentException("Starter não encontrado");

            starter.Name = starterDto.Name ?? starter.Name;            
            starter.Cpf = starterDto.Cpf ?? starter.Cpf;            
            starter.Abbreviation = starterDto.Abbreviation ?? starter.Abbreviation;            
            starter.Email = starterDto.Email ?? starter.Email;            
            starter.IsActive = starterDto.IsActive ?? starter.IsActive;
            if (starterDto.CategoryId != 0)
            {
                var category = _categoryRepository.GetByIdCategory(starterDto.CategoryId);
                if (category is null)
                    throw new ArgumentException("Categoria não encontrada");

                starter.Category = category;
            }

            _starterRepository.UpdateStarter(starter);
        }

        public void PutByIdStarter(StarterUpdateDto starterDto, int id)
        {
            if (id < 0)
                throw new ArgumentException("Id inválido");

            var starter = _starterRepository.GetByIdStarter(id);
            if (starter is null)
                throw new ArgumentException("Starter não encontrado");

            starter.Name = starterDto.Name;
            starter.Cpf = starterDto.Cpf;
            starter.Abbreviation = starterDto.Abbreviation;
            starter.Email = starterDto.Email;
            starter.IsActive = (bool)starterDto.IsActive;
            if (starterDto.CategoryId != starter.Category.Id)
            {
                var category = _categoryRepository.GetByIdCategory(starterDto.CategoryId);
                if (category is null)
                    throw new ArgumentException("Categoria não encontrada");

                starter.Category = category;
            }

            _starterRepository.UpdateStarter(starter);
        }

        public void DeleteByIdStarter(int id)
        {
            if (id < 0)
                throw new ArgumentException("Id inválido");

            var starter = _starterRepository.GetByIdStarter(id);
            if (starter is null)
                throw new ArgumentException("Starter não encontrado");

            _starterRepository.DeleteStarter(starter);
        }
    }
}