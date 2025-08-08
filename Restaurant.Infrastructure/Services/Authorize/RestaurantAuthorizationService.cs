using Microsoft.Extensions.Logging;
using Restaurant.Application.Features.Users.Interfaces;
using Restaurant.Domain.Constants;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Interfaces;

namespace Restaurant.Infrastructure.Services.Authorize;

public class RestaurantAuthorizationService(ILogger<RestaurantAuthorizationService> logger,
IUserContext userContext) : IRestaurantAuthorizationService
{
    public bool Authorize(RestaurantEntity restaurant, ResourceOperation resourceOperation)
    {
        var user = userContext.GetCurrentUser();

        logger.LogInformation("Authorizing user {UserEmail}, to {Operation} for restaurant {RestaurantName}",
            user!.Email,
            resourceOperation,
            restaurant.Name);

        if (resourceOperation == ResourceOperation.Read || resourceOperation == ResourceOperation.Create)
        {
            logger.LogInformation("Create/read operation - successful authorization");
            return true;
        }

        if (resourceOperation == ResourceOperation.Delete && (user.IsInRole(UserRoles.Admin) || user.IsInRole(UserRoles.SuperAdmin)))
        {
            logger.LogInformation("Admin user, delete operation - successful authorization");
            return true;
        }

        if ((resourceOperation == ResourceOperation.Delete || resourceOperation == ResourceOperation.Update)
        && user.Id == restaurant.OwnerId)
        {
            logger.LogInformation("Restaurant owner - successful authorization");
            return true;
        }

        return false;
    }
}