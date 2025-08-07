using MediatR;
using Restaurant.Application.User.Dtos;

namespace Restaurant.Application.Features.Users.Commands.RefreshToken;

public class RefreshTokenCommand : IRequest<ResponseDto>
{
    public string RefreshToken { get; set; } = default!;
}
