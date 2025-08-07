using MediatR;
using Restaurant.Application.Features.Categories.Dtos;

namespace Restaurant.Application.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQuery(int categoryId) : IRequest<CategoryDto>
    {
        public int CategoryId { get; } = categoryId;
    }
}
