using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Features.Users.Interfaces;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Repositories;
using Restaurant.Application.Restaurants.Dtos;

namespace Restaurant.Application.Features.Customers.Queries.GetCustomerFavoriteRestaurants;

public class GetCustomerFavoriteRestaurantsQueryHandler(ILogger<GetCustomerFavoriteRestaurantsQueryHandler> logger,
    ICustomerRepository customersRepository,
     ICustomerAuthorizationService customerAuthorizationService,
     IUserContext userContext,
     IMapper mapper) : IRequestHandler<GetCustomerFavoriteRestaurantsQuery, List<RestaurantDto>>
{
    public async Task<List<RestaurantDto>> Handle(GetCustomerFavoriteRestaurantsQuery request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        if (currentUser is null || currentUser.CustomerId is null)
            throw new UnauthorizedAccessException("User must be logged in as a customer.");

        int customerId = currentUser.CustomerId.Value;

        var customer = await customersRepository.GetByIdWithFavoritesAsync(customerId, cancellationToken)
                      ?? throw new NotFoundException(nameof(Customer), customerId.ToString());

        if (!customerAuthorizationService.CanAddToFavorites(customer))
            throw new ForbidException();

        var favorites = await customersRepository.GetFavoriteRestaurantsAsync(customerId);

        logger.LogInformation("Customer with Id : {CustId} has {Count} favorite restaurants",
                              customerId, favorites.Count);

        if (favorites == null || favorites.Count == 0)
            return [];

        return mapper.Map<List<RestaurantDto>>(favorites);
    }
}
