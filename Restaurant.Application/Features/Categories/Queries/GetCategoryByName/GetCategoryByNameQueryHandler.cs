using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Features.Categories.Dtos;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Categories.Queries.GetCategoryByName
{
    public class GetCategoryByNameQueryHandler(ILogger<GetCategoryByNameQueryHandler> logger,
     ICategoryRepository categoriesRepository,
     IMapper mapper) : IRequestHandler<GetCategoryByNameQuery, CategoryDto>
    {
        public async Task<CategoryDto> Handle(GetCategoryByNameQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting Category {CategoryName}", request.Name);

            var category = await categoriesRepository.GetByNameAsync(request.Name)
                    ?? throw new NotFoundNameException(nameof(Category), request.Name);

            var categoryDto = mapper.Map<CategoryDto>(category);

            return categoryDto;
        }
    }
}
