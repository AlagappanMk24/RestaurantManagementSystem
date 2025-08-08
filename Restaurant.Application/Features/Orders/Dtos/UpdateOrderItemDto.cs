namespace Restaurant.Application.Features.Orders.Dtos;

public class UpdateOrderItemDto
{
    public int OrderItemId { get; set; }
    public int DishId { get; set; }
    public int Quantity { get; set; }
}
