using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Constants;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Features.Ratings.Commands.DeleteRating;

public class DeleteRatingCommandHandler(ILogger<DeleteRatingCommandHandler> logger,
IRatingsRepository ratingsRepository,
IRatingAuthorizationService ratingAuthorizationService) : IRequestHandler<DeleteRatingCommand>
{
    public async Task Handle(DeleteRatingCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting rating with id: {RatingId}", request.Id);

        var rating = await ratingsRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Rating), request.Id.ToString());

        if (!ratingAuthorizationService.Authorize(rating, ResourceOperation.Delete))
            throw new ForbidException();

        await ratingsRepository.DeleteAsync(rating);
    }
}
