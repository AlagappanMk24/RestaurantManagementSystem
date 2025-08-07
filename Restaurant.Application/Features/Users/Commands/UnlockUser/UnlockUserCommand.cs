using MediatR;
using System.ComponentModel;

namespace Restaurant.Application.Features.Users.Commands.UnlockUser;

public class UnlockUserCommand(string email) : IRequest<string>
{
    [DefaultValue("user@gmail.com")]
    public string Email { get; } = email;
}
