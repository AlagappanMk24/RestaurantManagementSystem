using MediatR;
using Restaurant.Application.Features.Orders.Dtos;
using System.Text.Json.Serialization;

namespace Restaurant.Application.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderCommand : IRequest
{
    [JsonIgnore]
    public int Id { get; set; }

    public IList<UpdateOrderItemDto> Items { get; set; } = [];
}
