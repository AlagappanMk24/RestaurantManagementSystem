using MediatR;
using Restaurant.Application.User.Dtos;
using System.ComponentModel;

namespace Restaurant.Application.Features.Users.Commands.LoginUser;

public class LoginUserCommand : IRequest<ResponseDto>
{
    [DefaultValue("user@gmail.com")]
    public string Email { get; set; } = default!;

    [DefaultValue("SecurePass@123")]
    public string Password { get; set; } = default!;
}
