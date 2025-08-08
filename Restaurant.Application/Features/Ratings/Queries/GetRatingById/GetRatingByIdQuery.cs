using MediatR;
using Restaurant.Application.Features.Ratings.Dtos;

namespace Restaurant.Application.Features.Ratings.Queries.GetRatingById;

public class GetRatingByIdQuery(int id) : IRequest<RatingDto>
{
    public int Id { get; } = id;
}