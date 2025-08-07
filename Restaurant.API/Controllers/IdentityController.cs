using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Features.Users.Commands.AddRole;
using Restaurant.Application.Features.Users.Commands.AssignRoleToUser;
using Restaurant.Application.Features.Users.Commands.ChangePassword;
using Restaurant.Application.Features.Users.Commands.DeleteRole;
using Restaurant.Application.Features.Users.Commands.DeleteUser;
using Restaurant.Application.Features.Users.Commands.DisableRefreshToken;
using Restaurant.Application.Features.Users.Commands.ForgetPassword;
using Restaurant.Application.Features.Users.Commands.LoginUser;
using Restaurant.Application.Features.Users.Commands.RefreshToken;
using Restaurant.Application.Features.Users.Commands.RegisterMultipleUsers;
using Restaurant.Application.Features.Users.Commands.RegisterUser;
using Restaurant.Application.Features.Users.Commands.RemoveUserFromRole;
using Restaurant.Application.Features.Users.Commands.Resend2FACode;
using Restaurant.Application.Features.Users.Commands.ResetPassword;
using Restaurant.Application.Features.Users.Commands.Send2FACode;
using Restaurant.Application.Features.Users.Commands.UnlockUser;
using Restaurant.Application.Features.Users.Commands.UpdateRole;
using Restaurant.Application.Features.Users.Commands.UpdateUser;
using Restaurant.Application.Features.Users.Commands.Verify2FACode;
using Restaurant.Application.Features.Users.Queries.GetAllRoles;
using Restaurant.Application.Features.Users.Queries.GetAllUsers;
using Restaurant.Application.Features.Users.Queries.GetUserByEmail;
using Restaurant.Application.Features.Users.Queries.GetUserById;
using Restaurant.Application.Features.Users.Queries.GetUserByName;
using Restaurant.Application.Features.Users.Queries.GetUserByPhoneNumber;
using Restaurant.Application.Features.Users.Queries.GetUsersByRole;
using Restaurant.Domain.Constants;
using Restaurant.Application.User.Dtos;
using Restaurant.Application.User.Queries.GetRolesByEmail;

namespace Restaurant.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class IdentityController(IMediator mediator) : ControllerBase
    {
        [HttpPost("RegisterMultiple")]
        public async Task<IActionResult> RegisterMultiple([FromBody] RegisterMultipleUsersCommand command)
        {
            var results = await mediator.Send(command);

            var response = results.Select((res, index) => new
            {
                Index = index,
                Success = res.Succeeded,
                Errors = res.Errors?.Select(e => e.Description)
            });

            return Ok(response);
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            await mediator.Send(command);

            return Ok("User was registered ... Please login.");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var result = await mediator.Send(command);

            return Ok(result);
        }

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordCommand command)
        {
            var result = await mediator.Send(command);

            return Ok(result);
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            var result = await mediator.Send(command);

            return Ok(result);
        }

        [HttpPost("Send2FACode")]
        public async Task<IActionResult> Send2FACode([FromBody] Send2FACodeCommand command)
        {
            var result = await mediator.Send(command);

            return Ok(result);
        }

        [HttpPost("Resend2FACode")]
        public async Task<IActionResult> Resend2FACode([FromBody] Resend2FACodeCommand command)
        {
            var result = await mediator.Send(command);

            return Ok(result);
        }

        [HttpPost("Verify2FACode")]
        public async Task<IActionResult> Verify2FACode([FromBody] Verify2FACodeCommand command)
        {
            var result = await mediator.Send(command);

            return Ok(result);
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            var result = await mediator.Send(command);

            return Ok(result);
        }

        [Authorize(Roles = UserRoles.SuperAdmin)]
        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleToUserCommand command)
        {
            var result = await mediator.Send(command);

            return Ok(result);
        }

        [Authorize(Roles = UserRoles.SuperAdmin)]
        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole([FromBody] AddRoleCommand command)
        {
            await mediator.Send(command);

            return NoContent();
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("DisableRefreshToken")]
        public async Task<IActionResult> DisableRefreshToken([FromBody] DisableRefreshTokenCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll([FromQuery] GetAllUsersQuery query)
        {
            var users = await mediator.Send(query);
            return Ok(users);
        }

        [Authorize(Roles = UserRoles.SuperAdmin)]
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUserById([FromRoute] string id)
        {
            var user = await mediator.Send(new GetUserByIdQuery(id));
            return Ok(user);
        }

        [HttpGet("Email")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUserByEmail([FromQuery] string email = "user@gmail.com")
        {
            var user = await mediator.Send(new GetUserByEmailQuery(email));
            return Ok(user);
        }

        [HttpGet("Name")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUserByName([FromQuery] string name)
        {
            var user = await mediator.Send(new GetUserByNameQuery(name));
            return Ok(user);
        }

        [HttpGet("PhoneNumber")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUserByPhoneNumber([FromQuery] string phoneNumber)
        {
            var user = await mediator.Send(new GetUserByPhoneNumberQuery(phoneNumber));
            return Ok(user);
        }

        [Authorize(Roles = UserRoles.SuperAdmin)]
        [HttpPost("UnlockUser")]
        public async Task<IActionResult> UnlockUser([FromBody] UnlockUserCommand command)
        {
            var result = await mediator.Send(command);

            return Ok(result);
        }

        [Authorize(Roles = UserRoles.SuperAdmin)]
        [HttpDelete("Role")]
        public async Task<IActionResult> DeleteRole([FromQuery] string RoleName)
        {
            await mediator.Send(new DeleteRoleCommand(RoleName));

            return NoContent();
        }

        [Authorize(Roles = UserRoles.SuperAdmin)]
        [HttpDelete("UserFromRole")]
        public async Task<IActionResult> DeleteUserFromRole([FromBody] RemoveUserFromRoleCommand command)
        {
            var result = await mediator.Send(command);

            return Ok(result);
        }

        [Authorize(Roles = UserRoles.SuperAdmin)]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string Id)
        {
            await mediator.Send(new DeleteUserCommand(Id));
            return NoContent();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUser([FromRoute] string id, [FromBody] UpdateUserCommand command)
        {
            command.Id = id;
            await mediator.Send(command);
            return NoContent();
        }

        [HttpPatch("Role")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        [HttpGet("Roles")]
        [Authorize(Roles = UserRoles.SuperAdmin)]
        public async Task<ActionResult<IEnumerable<string>>> GetAllRoles()
        {
            var user = await mediator.Send(new GetAllRolesQuery());
            return Ok(user);
        }

        [HttpGet("RolesByEmail")]
        [Authorize(Roles = UserRoles.SuperAdmin)]
        public async Task<ActionResult<IEnumerable<string>>> GetAllRolesByEmail(string email)
        {
            var roles = await mediator.Send(new GetRolesByEmailQuery(email));
            return Ok(roles);
        }

        [HttpGet("Role")]
        [Authorize(Roles = UserRoles.SuperAdmin)]
        public ActionResult<IEnumerable<UserDto>> GetUsersByRole([FromRoute] string role, [FromQuery] string? searchPhrase = null,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 5,
        [FromQuery] string? sortBy = null,
        [FromQuery] SortDirection sortDirection = SortDirection.Ascending)
        {
            var query = new GetUsersByRoleQuery(role)
            {
                SearchPhrase = searchPhrase,
                PageNumber = pageNumber,
                PageSize = pageSize,
                SortBy = sortBy,
                SortDirection = sortDirection
            };
            return Ok(query);
        }
    }
}
