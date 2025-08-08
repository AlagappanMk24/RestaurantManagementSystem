using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Features.Customers.Commands.AddRestaurantToFavorites;
using Restaurant.Application.Features.Customers.Commands.CreateCustomer;
using Restaurant.Application.Features.Customers.Commands.CreateMultipleCustomers;
using Restaurant.Application.Features.Customers.Commands.DeleteCustomer;
using Restaurant.Application.Features.Customers.Commands.UpdateCustomer;
using Restaurant.Application.Features.Customers.Queries.GetAllCustomers;
using Restaurant.Application.Features.Customers.Queries.GetCustomerByEmail;
using Restaurant.Application.Features.Customers.Queries.GetCustomerById;
using Restaurant.Application.Features.Customers.Queries.GetCustomerByName;
using Restaurant.Application.Features.Customers.Queries.GetCustomerByPhoneNumber;
using Restaurant.Application.Features.Customers.Queries.GetCustomerFavoriteRestaurants;
using Restaurant.Domain.Exceptions;
using Restaurant.Application.Customers.Dtos;
using Restaurant.Application.Restaurants.Dtos;
using System.Data;

namespace Restaurant.API.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAll([FromQuery] GetAllCustomersQuery query)
        {
            var customers = await mediator.Send(query);
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto?>> GetById([FromRoute] int id)
        {
            var customer = await mediator.Send(new GetCustomerByIdQuery(id));
            return Ok(customer);
        }

        [HttpGet("Name")]
        public async Task<ActionResult<CustomerDto?>> GetByName([FromQuery] string name = "Ahmed Ali")
        {
            var customer = await mediator.Send(new GetCustomerByNameQuery(name));
            return Ok(customer);
        }

        [HttpGet("PhoneNumber")]
        public async Task<ActionResult<CustomerDto?>> GetByPhoneNumber([FromQuery] string phoneNumber = "0100000001")
        {
            var customer = await mediator.Send(new GetCustomerByPhoneNumberQuery(phoneNumber));
            return Ok(customer);
        }

        [HttpGet("Email")]
        public async Task<ActionResult<CustomerDto?>> GetByEmail([FromQuery] string email = "user@gmail.com")
        {
            var customer = await mediator.Send(new GetCustomerByEmailQuery(email));
            return Ok(customer);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            await mediator.Send(new DeleteCustomerCommand(id));
            return NoContent();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateRating([FromRoute] int id, [FromBody] UpdateCustomerCommand command)
        {
            command.Id = id;
            await mediator.Send(command);
            return NoContent();
        }

        [HttpPost("Multiple")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> CreateMultipleCustomers([FromBody] CreateMultipleCustomersCommand command)
        {
            var ids = await mediator.Send(command);
            return Ok(new { CreatedIds = ids });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command)
        {
            int id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPost("AddRestaurantToFavorite")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddFavorite([FromBody] AddRestaurantToFavoritesCommand command)
        {
            try
            {
                await mediator.Send(command);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (DuplicateNameException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("Favorites")]
        public async Task<ActionResult<List<RestaurantDto>>> GetFavorites()
        {
            var result = await mediator.Send(new GetCustomerFavoriteRestaurantsQuery());
            return Ok(result);
        }
    }
}
