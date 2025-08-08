using Restaurant.Domain.Constants;

namespace Restaurant.Domain.Interfaces;

public interface IRestaurantAuthorizationService
{
    bool Authorize(Entities.RestaurantEntity restaurant, ResourceOperation resourceOperation);
}