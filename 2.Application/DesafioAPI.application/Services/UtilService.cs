using System;
using System.Collections.Generic;
using System.Linq;
using DesafioAPI.application.Interfaces;
using DesafioAPI.domain.Entities;

namespace DesafioAPI.application.Services
{
    public class UserService : IUserService
    {
        public List<Starter> VerifyStartersIsActive(List<Starter> starter)
        {
            List<Starter> startersActive = starter.Where(e => e.IsActive).ToList();
            if (!startersActive.Any())
                throw new ArgumentException("Nenhum starter ativo");

            return startersActive;
        }

        public object HideStarterInformations(Starter starter)
        {
            return new
            {
                name = starter.Name, 
                abbreviation = starter.Abbreviation,
                email = starter.Email,
                photo = starter.Photo,
                category = $"{starter.Category.Name} - {starter.Category.Technology}" 
            };
        }
    }
}