using Restaurant.Application.Common;
using Restaurant.Application.Customers.Dtos;

namespace Restaurant.Application.Features.Orders.Dtos;

public class CustomerOrdersDto
{
    public CustomerDto Customer { get; set; } = default!;
    public PagedResult<OrderDto> OrdersPaged { get; set; } = default!;
}
