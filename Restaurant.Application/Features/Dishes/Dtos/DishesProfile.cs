using AutoMapper;
using Restaurant.Application.Features.Dishes.Commands.CreateDish;
using Restaurant.Domain.Entities;
using Restaurant.Application.Dishes.Commands.UpdateDish;
using Restaurant.Application.Dishes.Dtos;


namespace Restaurant.Application.Features.Dishes.Dtos;

public class DishesProfile : Profile
{
    public DishesProfile()
    {
        CreateMap<Dish, DishDto>()
             .ForMember(d => d.RestaurantName,
                opt => opt.MapFrom(src => src.Restaurant.Name))
              .ForMember(d => d.CategoryName,
                opt => opt.MapFrom(src => src.Category.Name));

        CreateMap<CreateDishCommand, Dish>();
        CreateMap<UpdateDishCommand, Dish>();
    }
}
