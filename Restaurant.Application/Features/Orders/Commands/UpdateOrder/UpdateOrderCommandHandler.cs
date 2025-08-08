using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandHandler(ILogger<UpdateOrderCommandHandler> logger,
IOrderRepository ordersRepository,
IDishesRepository dishesRepository,
 IOrderAuthorizationService orderAuthorizationService) : IRequestHandler<UpdateOrderCommand>
{
    public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await ordersRepository.GetByIdIncludeWithOrderItemsAsync(request.Id)
                   ?? throw new NotFoundException(nameof(Order), request.Id.ToString());

        if (!orderAuthorizationService.CanModifyOrder(order))
            throw new ForbidException();

        logger.LogInformation("Updating Order {Id}", request.Id);

        foreach (var item in request.Items)
        {
            if (item.OrderItemId > 0)
            {
                var orderItem = order.OrderItems
                                .FirstOrDefault(i => i.Id == item.OrderItemId)
                           ?? throw new NotFoundException(nameof(OrderItem), item.OrderItemId.ToString());

                item.Quantity = item.Quantity;
            }
            else
            {
                var dish = await dishesRepository.GetByIdAsync(item.DishId)
                           ?? throw new NotFoundException(nameof(Dish), item.DishId.ToString());

                order.OrderItems.Add(new OrderItem
                {
                    DishId = dish.Id,
                    Quantity = item.Quantity,
                    UnitPrice = dish.Price
                });
            }
        }

        order.TotalPrice = order.OrderItems.Sum(i => i.UnitPrice * i.Quantity);
        //order.Quantity = order.OrderItems.Sum(i => i.Quantity);

        await ordersRepository.SaveChanges();
    }
}
