using Microsoft.Extensions.Logging;
using Restaurant.Application.Features.Users.Interfaces;
using Restaurant.Domain.Constants;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Interfaces;

namespace Restaurant.Infrastructure.Services.Authorize;
public class CustomerAuthorizationService(
    ILogger<CustomerAuthorizationService> logger,
    IUserContext userContext) : ICustomerAuthorizationService
{
    public bool Authorize(Customer customer, ResourceOperation op)
    {
        var user = userContext.GetCurrentUser();
        if (user is null)
        {
            logger.LogWarning("Anonymous user attempted {Op} on customer {CustomerId}", op, customer.Id);
            return false;
        }

        if (user.IsInRole(UserRoles.Admin) || user.IsInRole(UserRoles.SuperAdmin))
            return true;

        if (op == ResourceOperation.Read && (customer.ApplicationUserId == user.Id) || user.CustomerId == customer.Id)
            return true;

        logger.LogWarning("Authorization failed ({Op}) by {User}", op, user.Email);
        return false;
    }

    public bool CanAddToFavorites(Customer customer)
    {
        var user = userContext.GetCurrentUser();
        if (user is null) return false;

        // Admin أو صاحب الحساب
        return user.IsInRole(UserRoles.Admin) || user.IsInRole(UserRoles.SuperAdmin) || user.CustomerId == customer.Id || customer.ApplicationUserId == user.Id;
    }
}
