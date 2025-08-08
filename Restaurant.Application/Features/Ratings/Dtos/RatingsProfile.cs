using AutoMapper;
using Restaurant.Application.Features.Ratings.Commands.CreateRating;
using Restaurant.Application.Features.Ratings.Commands.UpdateRating;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Features.Ratings.Dtos;

public class RatingsProfile : Profile
{
    public RatingsProfile()
    {
        CreateMap<Rating, RatingDto>()
               .ForMember(d => d.RestaurantName,
                opt => opt.MapFrom(src => src.Restaurant!.Name))
               .ForMember(d => d.DishName,
                opt => opt.MapFrom(src => src.Dish.Name))
               .ForMember(d => d.CustomerName,
                opt => opt.MapFrom(src => src.Customer.Name));

        CreateMap<CreateRatingCommand, Rating>();

        CreateMap<UpdateRatingCommand, Rating>();
    }
}
