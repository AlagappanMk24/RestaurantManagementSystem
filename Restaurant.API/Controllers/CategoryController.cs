using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Features.Categories.Commands.CreateCategory;
using Restaurant.Application.Features.Categories.Commands.DeleteCategory;
using Restaurant.Application.Features.Categories.Commands.UpdateCategory;
using Restaurant.Application.Features.Categories.Dtos;
using Restaurant.Application.Features.Categories.Queries.GetAllCategories;
using Restaurant.Application.Categories.Queries.GetCategoryById;
using Restaurant.Application.Categories.Queries.GetCategoryByName;
using Restaurant.Domain.Constants;

namespace Restaurant.API.Controllers;

[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize]
public class CategoryController(IMediator mediator) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll([FromQuery] GetAllCategoriesQuery query)
    {
        var categories = await mediator.Send(query);
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto?>> GetById([FromRoute] int id)
    {
        var category = await mediator.Send(new GetCategoryByIdQuery(id));
        return Ok(category);
    }

    [HttpGet("Name")]
    public async Task<ActionResult<CategoryDto?>> GetByName([FromQuery] string name)
    {
        var category = await mediator.Send(new GetCategoryByNameQuery(name));
        return Ok(category);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Roles = $"{UserRoles.Owner},{UserRoles.Admin}")]
    public async Task<IActionResult> DeleteCategory([FromRoute] int id)
    {
        await mediator.Send(new DeleteCategoryCommand(id));
        return NoContent();
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Roles = $"{UserRoles.Owner},{UserRoles.Admin}")]
    public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] UpdateCategoryCommand command)
    {
        command.Id = id;
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Roles = $"{UserRoles.Owner},{UserRoles.Admin}")]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
    {
        int id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }
}