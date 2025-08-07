using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Restaurant.Application.Features.Users.Queries.GetAllRoles;

public class GetAllRolesQueryHandler(ILogger<GetAllRolesQueryHandler> logger,
    RoleManager<IdentityRole> roleManager) : IRequestHandler<GetAllRolesQuery, IEnumerable<string>>
{
    public async Task<IEnumerable<string>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get All Roles");

        var roles = await roleManager.Roles.Select(r => r.Name).ToListAsync(cancellationToken: cancellationToken);
        if (roles == null || roles.Count == 0)
            throw new BadRequestException("No Roles Exist!");

        return roles!;
    }
}
