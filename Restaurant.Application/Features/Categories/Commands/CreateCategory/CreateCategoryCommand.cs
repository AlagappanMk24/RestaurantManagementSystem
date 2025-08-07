using MediatR;
using System.ComponentModel;

namespace Restaurant.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<int>
    {
        [DefaultValue("Drinks")]
        public string Name { get; set; } = default!;

        [DefaultValue("Write Description")]
        public string? Description { get; set; }
    }
}
