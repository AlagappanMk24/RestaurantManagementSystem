using MediatR;
using Restaurant.Application.Restaurants.Dtos;

namespace Restaurant.Application.Features.Customers.Queries.GetCustomerFavoriteRestaurants;

public class GetCustomerFavoriteRestaurantsQuery : IRequest<List<RestaurantDto>>
{
}