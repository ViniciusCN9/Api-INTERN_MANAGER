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
            try
            {
                List<Starter> startersActive = starter.Where(e => e.IsActive).ToList();
                if (!startersActive.Any())
                    throw new ArgumentException("Nenhum starter ativo");
                
                return startersActive;
            }
            catch
            {
                throw new Exception("Erro ao verificar status");
            }
        }

        public object HideStarterInformations(Starter starter)
        {
            try
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
            catch
            {
                throw new Exception("Erro ao esconder informações");
            }
        }
    }
}