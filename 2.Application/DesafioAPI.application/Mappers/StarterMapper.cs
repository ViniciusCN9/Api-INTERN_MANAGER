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
            Abbreviation = starterDto.Abbreviation.ToLower(),
            Email = starterDto.Email,
            Photo = "Default.jpg",
            IsActive = true
        };
    }
}