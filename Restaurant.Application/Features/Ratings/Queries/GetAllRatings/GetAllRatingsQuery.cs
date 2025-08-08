using MediatR;
using Restaurant.Application.Common;
using Restaurant.Application.Features.Ratings.Dtos;
using Restaurant.Domain.Constants;

namespace Restaurant.Application.Features.Ratings.Queries.GetAllRatings;

public class GetAllRatingsQuery : IRequest<PagedResult<RatingDto>>
{
    public string? SearchPhrase { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 5;
    public string? SortBy { get; set; }
    public SortDirection SortDirection { get; set; }
}
