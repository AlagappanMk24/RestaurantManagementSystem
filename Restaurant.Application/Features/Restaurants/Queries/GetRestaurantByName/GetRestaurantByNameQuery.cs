using MediatR;
using Restaurant.Application.Restaurants.Dtos;

namespace Restaurant.Application.Restaurants.Queries.GetRestaurantByName
{
    public class GetRestaurantByNameQuery(string name) : IRequest<RestaurantDto>
    {
        public string Name { get; } = name;
    }
}
