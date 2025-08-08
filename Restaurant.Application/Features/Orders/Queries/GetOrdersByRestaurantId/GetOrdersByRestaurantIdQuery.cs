using MediatR;
using Restaurant.Application.Features.Orders.Dtos;
using Restaurant.Domain.Constants;

namespace Restaurant.Application.Features.Orders.Queries.GetOrdersByRestaurantId;

public class GetOrdersByRestaurantIdQuery(int restaurantId) : IRequest<RestaurantOrdersDto>
{
    public int RestaurantId { get; } = restaurantId;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 5;
    public string? SortBy { get; set; }
    public SortDirection SortDirection { get; set; }
}
