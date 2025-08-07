using MediatR;
using Restaurant.Application.Features.Categories.Dtos;

namespace Restaurant.Application.Categories.Queries.GetCategoryByName
{
    public class GetCategoryByNameQuery(string name) : IRequest<CategoryDto>
    {
        public string Name { get; } = name;
    }
}
