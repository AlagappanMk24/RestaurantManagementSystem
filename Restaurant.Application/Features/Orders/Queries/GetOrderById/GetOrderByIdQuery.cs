using MediatR;
using Restaurant.Application.Features.Orders.Dtos;

namespace Restaurant.Application.Features.Orders.Queries.GetOrderById;
public class GetOrderByIdQuery(int id) : IRequest<OrderDto>
{
    public int Id { get; } = id;
}
