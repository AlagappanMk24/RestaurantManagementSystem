using Microsoft.AspNetCore.Http;
using Restaurant.Application.Features.Users.Interfaces;
using System.Security.Claims;

namespace Restaurant.Application.Features.Users.Implementations
{
    public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
    {
        public CurrentUser? GetCurrentUser()
        {
            var user = httpContextAccessor?.HttpContext?.User
                ?? throw new InvalidOperationException("User context is not present");

            if (user.Identity == null || !user.Identity.IsAuthenticated)
                return null;

            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = user.FindFirst(ClaimTypes.Email)?.Value;
            var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
            var customerIdClaim = user.FindFirst("CustomerId")?.Value;

            int? customerId = null;
            if (int.TryParse(customerIdClaim, out var parsedId))
            {
                customerId = parsedId;
            }

            return new CurrentUser(userId!, email!, roles, customerId);
        }
    }
}
