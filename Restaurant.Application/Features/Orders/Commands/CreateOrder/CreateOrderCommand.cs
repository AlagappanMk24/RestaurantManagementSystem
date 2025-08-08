using MediatR;
using Restaurant.Application.Features.Orders.Dtos;
using System.ComponentModel;

namespace Restaurant.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<int>
{
    [DefaultValue(500)]
    public int CustomerId { get; set; }

    public List<CreateOrderItemDto> Items { get; set; } = [];
}
