using MediatR;

namespace Restaurant.Application.Features.Users.Queries.GetAllRoles;

public class GetAllRolesQuery : IRequest<IEnumerable<string>>
{
}
