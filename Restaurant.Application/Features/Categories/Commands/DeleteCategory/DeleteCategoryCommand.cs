using MediatR;

namespace Restaurant.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand(int id) : IRequest
    {
        public int Id { get; } = id;
    }
}
