using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;
using Restaurant.Application.Restaurants.Dtos;
using Restaurant.Application.Restaurants.Queries.GetRestaurantStatistics;

namespace Restaurant.Application.Features.Restaurants.Queries.GetRestaurantStatistics;

public class GetRestaurantStatisticsQueryHandler(ILogger<GetRestaurantStatisticsQueryHandler> logger,
    IRestaurantRepository restaurantsRepository, IMapper mapper)
: IRequestHandler<GetRestaurantStatisticsQuery, RestaurantStatisticsDto>
{
    public async Task<RestaurantStatisticsDto> Handle(GetRestaurantStatisticsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting Statistics For Restaurant {RestaurantId}", request.RestaurantId);

        var restaurant = await restaurantsRepository.GetStatisticsAsync(request.RestaurantId)
            ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var restaurantStatisticsDto = mapper.Map<RestaurantStatisticsDto>(restaurant);

        restaurantStatisticsDto.TotalOrders = await restaurantsRepository.GetTotalOrdersForRestaurantAsync(request.RestaurantId);

        restaurantStatisticsDto.TotalRevenue =
            await restaurantsRepository.GetTotalRevenueForRestaurantAsync(request.RestaurantId);

        restaurantStatisticsDto.MostPopularDishName =
            await restaurantsRepository.GetMostPopularDishNameAsync(request.RestaurantId);

        restaurantStatisticsDto.LastOrderDate =
            await restaurantsRepository.GetLastOrderDateAsync(request.RestaurantId);

        return restaurantStatisticsDto;
    }
}
