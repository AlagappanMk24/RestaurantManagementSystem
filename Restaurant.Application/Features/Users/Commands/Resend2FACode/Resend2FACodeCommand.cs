using MediatR;
using System.ComponentModel;

namespace Restaurant.Application.Features.Users.Commands.Resend2FACode;

public class Resend2FACodeCommand(string email) : IRequest<string>
{
    [DefaultValue("user@gmail.com")]
    public string Email { get; } = email;
}
