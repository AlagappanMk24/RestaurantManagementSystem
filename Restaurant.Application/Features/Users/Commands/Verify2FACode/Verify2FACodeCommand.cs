using MediatR;
using System.ComponentModel;

namespace Restaurant.Application.Features.Users.Commands.Verify2FACode;

public class Verify2FACodeCommand(string email) : IRequest<string>
{
    [DefaultValue("user@example.com")]
    public string Email { get; } = email;

    [DefaultValue("123456")]
    public string Code { get; set; } = default!;
}
