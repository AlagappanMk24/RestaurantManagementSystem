using AutoMapper;
using Restaurant.Application.Features.Categories.Commands.CreateCategory;
using Restaurant.Application.Features.Categories.Commands.UpdateCategory;
using Restaurant.Application.Features.Categories.Dtos;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Features.Categories.Mapping
{
    public class CategoriesProfile : Profile
    {
        public CategoriesProfile()
        {
            CreateMap<Category, CategoryDto>();

            CreateMap<CreateCategoryCommand, Category>();

            CreateMap<UpdateCategoryCommand, Category>();
        }
    }
}
