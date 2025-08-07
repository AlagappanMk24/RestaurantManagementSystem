using MediatR;
using Restaurant.Application.User.Dtos;

namespace Restaurant.Application.Features.Users.Queries.GetUserById;

public class GetUserByIdQuery(string id) : IRequest<UserDto>
{
    public string Id { get; } = id;
}
