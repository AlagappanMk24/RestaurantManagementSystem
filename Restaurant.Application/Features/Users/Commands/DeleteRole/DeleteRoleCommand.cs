using MediatR;
using System.ComponentModel;

namespace Restaurant.Application.Features.Users.Commands.DeleteRole;
public class DeleteRoleCommand(string roleName) : IRequest
{
    [DefaultValue("User")]
    public string RoleName { get; } = roleName;
}
