using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAPI.application.DataTransferObjects;
using DesafioAPI.domain.Entities;

namespace DesafioAPI.application.Mappers
{
    public static class StarterMapper
    {
        public static Starter ToDomain(this StarterCreateDto starterDto) => new Starter
        {
            Name = starterDto.Name,
            Cpf = starterDto.Cpf,
            Abbreviation = starterDto.Abbreviation,
            Email = starterDto.Email,
            Photo = "Default.jpg",
            IsActive = true
        };
    }
}