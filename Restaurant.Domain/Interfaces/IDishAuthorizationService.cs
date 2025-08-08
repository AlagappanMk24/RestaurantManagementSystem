using Restaurant.Domain.Constants;
using Restaurant.Domain.Entities;

namespace Restaurant.Domain.Interfaces;

public interface IDishAuthorizationService
{
    bool Authorize(RestaurantEntity restaurant, ResourceOperation operation);

    bool Authorize(Dish dish, ResourceOperation operation);
}