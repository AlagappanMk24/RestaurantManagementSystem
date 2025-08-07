using MediatR;
using Restaurant.Application.Common;
using Restaurant.Domain.Constants;
using Restaurant.Application.Dishes.Dtos;

namespace Restaurant.Application.Features.Dishes.Queries.GetDishesForRestaurant;

public class GetDishesForRestaurantQuery(int restaurantId) : IRequest<PagedResult<DishDto>>
{
    public int RestaurantId { get; } = restaurantId;
    public string? SearchPhrase { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 5;
    public string? SortBy { get; set; }
    public SortDirection SortDirection { get; set; }
}
