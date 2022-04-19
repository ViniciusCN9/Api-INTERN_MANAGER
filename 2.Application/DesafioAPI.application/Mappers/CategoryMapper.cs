using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAPI.application.DataTransferObjects;
using DesafioAPI.domain.Entities;

namespace DesafioAPI.application.Mappers
{
    public static class CategoryMapper
    {
        public static Category ToDomain(this CategoryDto categoryDto) => new Category
        {
            Name = categoryDto.Name,
            Technology = categoryDto.Technology,
            IsActive = true
        };
    }
}