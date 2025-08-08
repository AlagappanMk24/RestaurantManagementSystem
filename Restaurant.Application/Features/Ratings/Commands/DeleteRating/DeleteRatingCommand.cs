using MediatR;

namespace Restaurant.Application.Features.Ratings.Commands.DeleteRating;
public class DeleteRatingCommand(int id) : IRequest
{
    public int Id { get; } = id;
}
