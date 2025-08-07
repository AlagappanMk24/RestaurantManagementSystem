using MediatR;
using Restaurant.Application.User.Dtos;

namespace Restaurant.Application.Features.Users.Queries.GetUserByEmail;

public class GetUserByEmailQuery(string email) : IRequest<UserDto>
{
    public string Email { get; } = email;
}
