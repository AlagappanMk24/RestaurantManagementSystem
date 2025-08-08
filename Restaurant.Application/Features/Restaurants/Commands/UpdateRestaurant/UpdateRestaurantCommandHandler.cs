using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Constants;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Features.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger,
IRestaurantRepository restaurantsRepository,
IRestaurantAuthorizationService restaurantAuthorizationService,
IMapper mapper) : IRequestHandler<UpdateRestaurantCommand>
{
    public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating restaurant with id: {RestaurantId} with {@UpdatedRestaurant}", request.Id, request);

        var restaurant = await restaurantsRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
            throw new ForbidException();

        mapper.Map(request, restaurant);

        await restaurantsRepository.SaveChanges();
    }
}
