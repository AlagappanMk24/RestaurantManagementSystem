using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Features.Orders.Dtos;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Features.Orders.Queries.GetOrderById;

public class GetOrderByIdQueryHandler(ILogger<GetOrderByIdQueryHandler> logger,
 IOrderRepository ordersRepository,
 IOrderAuthorizationService orderAuthorizationService,
 IMapper mapper) : IRequestHandler<GetOrderByIdQuery, OrderDto>
{
    public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await ordersRepository.GetByIdIncludeWithOrderItemsAsync(request.Id)
                ?? throw new NotFoundException(nameof(Order), request.Id.ToString());

        if (!orderAuthorizationService.CanViewCustomerOrder(order.Customer.ApplicationUserId))
            throw new ForbidException();

        logger.LogInformation("Getting order {OrderId}", request.Id);

        var orderDto = mapper.Map<OrderDto>(order);

        return orderDto;
    }
}
