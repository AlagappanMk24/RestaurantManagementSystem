using MediatR;
using Restaurant.Application.User.Dtos;

namespace Restaurant.Application.Features.Users.Commands.DisableRefreshToken;

public class DisableRefreshTokenCommand : IRequest<ResponseDto>
{
    public string RefreshToken { get; set; } = default!;
}
