using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetRestaurantByName;

namespace Restaurant.Application.Features.Restaurants.Queries.GetRestaurantByName;

public class GetRestaurantByNameQueryHandler(ILogger<GetRestaurantByNameQueryHandler> logger,
 IRestaurantRepository restaurantsRepository,
 IMapper mapper) : IRequestHandler<GetRestaurantByNameQuery, RestaurantDto>
{
    public async Task<RestaurantDto> Handle(GetRestaurantByNameQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting Restaurant {RestaurantName}", request.Name);

        var restaurant = await restaurantsRepository.GetByNameAsync(request.Name)
                ?? throw new NotFoundNameException(nameof(Restaurant), request.Name);

        var restaurantDto = mapper.Map<RestaurantDto>(restaurant);

        return restaurantDto;
    }
}
