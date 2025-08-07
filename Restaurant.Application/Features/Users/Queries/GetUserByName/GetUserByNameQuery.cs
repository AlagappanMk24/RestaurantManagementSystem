using MediatR;
using Restaurant.Application.User.Dtos;

namespace Restaurant.Application.Features.Users.Queries.GetUserByName;

public class GetUserByNameQuery(string fullName) : IRequest<UserDto>
{
    public string FullName { get; } = fullName;
}
