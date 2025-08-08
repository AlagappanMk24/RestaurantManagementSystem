using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Features.Ratings.Commands.CreateRating;
using Restaurant.Application.Features.Ratings.Commands.DeleteRating;
using Restaurant.Application.Features.Ratings.Commands.UpdateRating;
using Restaurant.Application.Features.Ratings.Dtos;
using Restaurant.Application.Features.Ratings.Queries.GetAllRatings;
using Restaurants.Application.Ratings.Queries.GetRatingById;

namespace Restaurant.API.Controllers
{
    [ApiVersion("3.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class RatingsController(IMediator mediator) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RatingDto>>> GetAll([FromQuery] GetAllRatingsQuery query)
        {
            var ratings = await mediator.Send(query);
            return Ok(ratings);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RatingDto?>> GetById([FromRoute] int id)
        {
            var rating = await mediator.Send(new GetRatingByIdQuery(id));
            return Ok(rating);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRating([FromRoute] int id)
        {
            await mediator.Send(new DeleteRatingCommand(id));
            return NoContent();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateRating([FromRoute] int id, [FromBody] UpdateRatingCommand command)
        {
            command.Id = id;
            await mediator.Send(command);
            return NoContent();
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateRating([FromBody] CreateRatingCommand command)
        {
            int id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }
    }
}
