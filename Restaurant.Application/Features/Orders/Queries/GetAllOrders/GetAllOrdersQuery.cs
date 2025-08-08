using MediatR;
using Restaurant.Application.Common;
using Restaurant.Application.Features.Orders.Dtos;
using Restaurant.Domain.Constants;

namespace Restaurant.Application.Features.Orders.Queries.GetAllOrders;

public class GetAllOrdersQuery : IRequest<PagedResult<OrderDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 5;
    public string? SortBy { get; set; }
    public SortDirection SortDirection { get; set; }
}
