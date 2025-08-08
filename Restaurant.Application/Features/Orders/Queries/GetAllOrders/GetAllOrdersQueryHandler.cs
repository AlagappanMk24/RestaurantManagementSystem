using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Common;
using Restaurant.Application.Features.Orders.Dtos;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Features.Orders.Queries.GetAllOrders;

public class GetAllOrdersQueryHandler(ILogger<GetAllOrdersQueryHandler> logger,
 IMapper mapper,
 IOrderRepository ordersRepository,
 IOrderAuthorizationService orderAuthorizationService
  ) : IRequestHandler<GetAllOrdersQuery, PagedResult<OrderDto>>
{
    public async Task<PagedResult<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var (orders, totalCount) = await ordersRepository.GetAllMatchingAsync(request.PageSize,
            request.PageNumber,
            request.SortBy,
            request.SortDirection);

        if (!orderAuthorizationService.CanViewAllOrders())
            throw new ForbidException();

        logger.LogInformation("Getting all orders");

        var ordersDtos = mapper.Map<IEnumerable<OrderDto>>(orders);

        var result = new PagedResult<OrderDto>(ordersDtos, totalCount, request.PageSize, request.PageNumber);
        return result;
    }
}
