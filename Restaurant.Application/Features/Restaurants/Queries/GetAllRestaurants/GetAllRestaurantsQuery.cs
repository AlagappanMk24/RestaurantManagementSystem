using MediatR;
using Restaurant.Application.Common;
using Restaurant.Domain.Constants;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQuery : IRequest<PagedResult<RestaurantDto>>
    {
        public string? SearchPhrase { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public string? SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
