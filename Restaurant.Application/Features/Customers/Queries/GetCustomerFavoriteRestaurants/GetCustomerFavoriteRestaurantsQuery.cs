using MediatR;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurant.Application.Features.Customers.Queries.GetCustomerFavoriteRestaurants;

public class GetCustomerFavoriteRestaurantsQuery : IRequest<List<RestaurantDto>>
{
}