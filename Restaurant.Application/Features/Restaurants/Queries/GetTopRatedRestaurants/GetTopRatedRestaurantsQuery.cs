using MediatR;
using Restaurant.Application.Restaurants.Dtos;

namespace Restaurant.Application.Restaurants.Queries.GetTopRatedRestaurants
{
    public class GetTopRatedRestaurantsQuery(int count) : IRequest<List<RestaurantDto>>
    {
        public int Count { get; } = count;
    }

}
