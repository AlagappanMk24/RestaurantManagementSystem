using Microsoft.AspNetCore.Identity;
using Restaurant.Domain.Constants;
using Restaurant.Infrastructure.Data.Context;
using Restaurant.Infrastructure.Seeders;

namespace Restaurant.Infrastructure.Seeders;

public class RoleSeeder(RestaurantDbContext dbContext) : IRoleSeeder
{
    public async Task Seed()
    {
        if (!dbContext.Roles.Any())
        {
            var roles = GetRoles();
            dbContext.Roles.AddRange(roles);
            await dbContext.SaveChangesAsync();
        }
    }
    private IEnumerable<IdentityRole> GetRoles()
    {
        List<IdentityRole> roles =
            [
                new (UserRoles.User)
            {
                NormalizedName = UserRoles.User.ToUpper()
            },
            new (UserRoles.Owner)
            {
                NormalizedName = UserRoles.Owner.ToUpper()
            },
            new (UserRoles.Admin)
            {
                NormalizedName = UserRoles.Admin.ToUpper()
            },
            new (UserRoles.SuperAdmin)
            {
                NormalizedName = UserRoles.SuperAdmin.ToUpper()
            }
        ];

        return roles;
    }
}