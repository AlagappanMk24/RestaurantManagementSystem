using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler(ILogger<DeleteCategoryCommandHandler> logger,
      ICategoryAuthorizationService categoryAuthorizationService,
      ICategoryRepository categoriesRepository) : IRequestHandler<DeleteCategoryCommand>
    {
        public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await categoriesRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(Category), request.Id.ToString());

            if (!categoryAuthorizationService.CanModifyCategory(category))
                throw new ForbidException();

            logger.LogInformation("Deleting Category with id: {CategoryId}", request.Id);

            await categoriesRepository.DeleteAsync(category);
        }
    }
}
