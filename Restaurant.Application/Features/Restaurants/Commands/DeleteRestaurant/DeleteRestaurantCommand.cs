using MediatR;

namespace Restaurant.Application.Features.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommand(int id) : IRequest
    {
        public int Id { get; } = id;
    }
}