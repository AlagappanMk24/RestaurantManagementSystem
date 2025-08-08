using Restaurant.Application.Common;
using Restaurant.Application.Restaurants.Dtos;

namespace Restaurant.Application.Features.Orders.Dtos
{
    public class RestaurantOrdersDto
    {
        public RestaurantDto Restaurant { get; set; } = default!;
        public PagedResult<OrderDto> OrdersPaged { get; set; } = default!;
    }
}
