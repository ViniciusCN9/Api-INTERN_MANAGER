using DesafioAPI.application.DataTransferObjects;
using DesafioAPI.domain.Entities;

namespace DesafioAPI.application.Mappers
{
    public static class CategoryMapper
    {
        public static Category ToDomain(this CategoryCreateDto categoryDto) => new Category
        {
            Name = categoryDto.Name.ToUpper(),
            Technology = categoryDto.Technology.ToUpper(),
            IsActive = true
        };
    }
}