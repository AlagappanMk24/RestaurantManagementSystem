using MediatR;
using Restaurant.Application.Restaurants.Dtos;

namespace Restaurant.Application.Restaurants.Queries.GetRestaurantStatistics
{
    public class GetRestaurantStatisticsQuery(int restaurantId) : IRequest<RestaurantStatisticsDto>
    {
        public int RestaurantId { get; } = restaurantId;
    }
}
