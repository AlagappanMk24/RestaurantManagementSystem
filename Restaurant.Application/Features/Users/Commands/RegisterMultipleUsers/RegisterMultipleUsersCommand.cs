using MediatR;
using Microsoft.AspNetCore.Identity;
using Restaurant.Application.Features.Users.Commands.RegisterUser;

namespace Restaurant.Application.Features.Users.Commands.RegisterMultipleUsers;

public class RegisterMultipleUsersCommand : IRequest<List<IdentityResult>>
{
    public List<RegisterUserCommand> Users { get; set; } = [];
}
