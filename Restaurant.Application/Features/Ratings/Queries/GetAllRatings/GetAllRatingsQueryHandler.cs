using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Common;
using Restaurant.Application.Features.Ratings.Dtos;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Features.Ratings.Queries.GetAllRatings;

public class GetAllRatingsQueryHandler(ILogger<GetAllRatingsQueryHandler> logger,
 IMapper mapper,
 IRatingsRepository ratingsRepository) : IRequestHandler<GetAllRatingsQuery, PagedResult<RatingDto>>
{
    public async Task<PagedResult<RatingDto>> Handle(GetAllRatingsQuery request, CancellationToken cancellationToken)
    {
        var (ratings, totalCount) = await ratingsRepository.GetAllMatchingAsync(request.SearchPhrase,
        request.PageSize,
        request.PageNumber,
        request.SortBy,
        request.SortDirection);

        var ratingDtos = mapper.Map<IEnumerable<RatingDto>>(ratings);

        var result = new PagedResult<RatingDto>(ratingDtos, totalCount, request.PageSize, request.PageNumber);

        logger.LogInformation("Getting all Ratings");

        return result;
    }
}
