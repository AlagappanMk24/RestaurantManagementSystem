using MediatR;

namespace Restaurant.Application.Features.Users.Commands.DeleteUser;

public class DeleteUserCommand(string id) : IRequest
{
    public string Id { get; } = id!;
}
