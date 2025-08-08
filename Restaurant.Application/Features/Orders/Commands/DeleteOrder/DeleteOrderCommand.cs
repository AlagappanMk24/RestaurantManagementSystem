using MediatR;

namespace Restaurant.Application.Features.Orders.Commands.DeleteOrder;

public class DeleteOrderCommand(int id) : IRequest
{
    public int Id { get; } = id;
}
