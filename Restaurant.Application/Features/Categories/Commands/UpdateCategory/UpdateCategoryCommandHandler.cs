using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Features.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler(ILogger<UpdateCategoryCommandHandler> logger,
ICategoryRepository categoriesRepository,
ICategoryAuthorizationService categoryAuthorizationService,
IMapper mapper) : IRequestHandler<UpdateCategoryCommand>
{
    public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoriesRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Category), request.Id.ToString());

        if (!categoryAuthorizationService.CanModifyCategory(category))
            throw new ForbidException();

        mapper.Map(request, category);

        logger.LogInformation("Updating Category with id: {CategoryId} with {@UpdatedCategory}", request.Id, request);

        await categoriesRepository.SaveChanges();
    }
}
