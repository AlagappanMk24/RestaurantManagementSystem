using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Features.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandHandler(ILogger<DeleteOrderCommandHandler> logger,
IOrderAuthorizationService orderAuthorizationService,
IOrderRepository ordersRepository) : IRequestHandler<DeleteOrderCommand>
{
    public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await ordersRepository.GetByIdIncludeWithOrderItemsAsync(request.Id)
                    ?? throw new NotFoundException(nameof(Order), request.Id.ToString());

        if (!orderAuthorizationService.CanModifyOrder(order))
            throw new ForbidException();

        logger.LogInformation("Deleting order with Id: {OrderId}", request.Id);

        await ordersRepository.DeleteAsync(order);
    }
}
