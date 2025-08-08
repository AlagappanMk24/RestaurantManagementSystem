using MediatR;

namespace Restaurant.Application.Restaurants.Queries.GetCategoriesForRestaurant
{
    public class GetCategoriesForRestaurantQuery(int restaurantId) : IRequest<List<string>>
    {
        public int RestaurantId { get; } = restaurantId;
    }
}
