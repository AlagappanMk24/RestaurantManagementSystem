using MediatR;
using Restaurant.Application.Common;
using Restaurant.Application.Features.Categories.Dtos;
using Restaurant.Domain.Constants;

namespace Restaurant.Application.Features.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesQuery : IRequest<PagedResult<CategoryDto>>
    {
        public string? SearchPhrase { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public string? SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }

}
