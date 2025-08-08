using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Common;
using Restaurant.Application.Features.Orders.Dtos;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Repositories;
using Restaurant.Application.Restaurants.Dtos;

namespace Restaurant.Application.Features.Orders.Queries.GetOrdersByRestaurantId;

public class GetOrdersByRestaurantIdQueryHandler(ILogger<GetOrdersByRestaurantIdQueryHandler> logger,
 IMapper mapper,
 IOrderRepository ordersRepository,
 IRestaurantRepository restaurantsRepository,
 IOrderAuthorizationService orderAuthorizationService) : IRequestHandler<GetOrdersByRestaurantIdQuery, RestaurantOrdersDto>
{
    public async Task<RestaurantOrdersDto> Handle(GetOrdersByRestaurantIdQuery request, CancellationToken cancellationToken)
    {
        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId)
                 ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        if (!orderAuthorizationService.CanViewRestaurantOrders(restaurant.OwnerId!))
            throw new ForbidException();

        var (orders, totalCount) = await ordersRepository.GetAllMatchingAsync(request.PageSize,
            request.PageNumber,
            request.SortBy,
            request.SortDirection);

        var ordersByRestaurant = orders.Where(o => o.OrderItems.Any(oi => oi.Dish.RestaurantId == request.RestaurantId));

        logger.LogInformation("Getting all orders By Restaurant {RestaurantId}", request.RestaurantId);

        var pagedOrders = new PagedResult<OrderDto>(
            mapper.Map<List<OrderDto>>(ordersByRestaurant),
            ordersByRestaurant.Count(),
            request.PageSize,
            request.PageNumber);

        return new RestaurantOrdersDto
        {
            Restaurant = mapper.Map<RestaurantDto>(restaurant),
            OrdersPaged = pagedOrders
        };

    }
}
