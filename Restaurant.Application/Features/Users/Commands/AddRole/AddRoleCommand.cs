using MediatR;

namespace Restaurant.Application.Features.Users.Commands.AddRole;

public class AddRoleCommand(string roleName) : IRequest
{
    public string RoleName { get; } = roleName;
}
