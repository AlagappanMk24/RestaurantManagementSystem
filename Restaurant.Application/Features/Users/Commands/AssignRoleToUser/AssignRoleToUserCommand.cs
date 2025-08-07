using MediatR;
using System.ComponentModel;

namespace Restaurant.Application.Features.Users.Commands.AssignRoleToUser;
public class AssignRoleToUserCommand : IRequest<string>
{
    [DefaultValue("user@gmail.com")]
    public string Email { get; set; } = default!;

    [DefaultValue("Admin")]
    public string UserType { get; set; } = default!;
}
