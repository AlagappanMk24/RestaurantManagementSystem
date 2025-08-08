using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Common;
using Restaurant.Domain.Repositories;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

namespace Restaurant.Application.Features.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryHandler(ILogger<GetAllRestaurantsQueryHandler> logger, IMapper mapper, IRestaurantRepository restaurantsRepository) : IRequestHandler<GetAllRestaurantsQuery, PagedResult<RestaurantDto>>
{
    public async Task<PagedResult<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all restaurants");
        var (restaurants, totalCount) = await restaurantsRepository.GetAllMatchingAsync(request.SearchPhrase,
            request.PageSize,
            request.PageNumber,
            request.SortBy,
            request.SortDirection);

        var restaurantsDtos = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

        var result = new PagedResult<RestaurantDto>(restaurantsDtos, totalCount, request.PageSize, request.PageNumber);
        return result;
    }
}