using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Common;
using Restaurant.Application.Features.Categories.Dtos;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Features.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesQueryHandler(ILogger<GetAllCategoriesQueryHandler> logger,
    ICategoryRepository categoriesRepository,
    IMapper mapper) : IRequestHandler<GetAllCategoriesQuery, PagedResult<CategoryDto>>
    {
        async Task<PagedResult<CategoryDto>> IRequestHandler<GetAllCategoriesQuery, PagedResult<CategoryDto>>.Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Retrieving All Categories");

            var (categories, totalCount) = await categoriesRepository.GetAllMatchingAsync(request.SearchPhrase,
               request.PageSize,
               request.PageNumber,
               request.SortBy,
               request.SortDirection);

            var categoriesDtos = mapper.Map<IEnumerable<CategoryDto>>(categories);

            var result = new PagedResult<CategoryDto>(categoriesDtos, totalCount, request.PageSize, request.PageNumber);
            return result;
        }
    }

}
