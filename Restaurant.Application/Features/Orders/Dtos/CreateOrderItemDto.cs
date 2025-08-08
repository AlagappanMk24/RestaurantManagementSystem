using System.ComponentModel;

namespace Restaurant.Application.Features.Orders.Dtos;

public class CreateOrderItemDto
{
    [DefaultValue(620)]
    public int DishId { get; set; }

    [DefaultValue(2)]
    public int Quantity { get; set; }
}
