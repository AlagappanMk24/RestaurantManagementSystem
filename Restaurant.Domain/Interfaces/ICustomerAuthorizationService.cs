using Restaurant.Domain.Constants;
using Restaurant.Domain.Entities;

namespace Restaurant.Domain.Interfaces
{
    public interface ICustomerAuthorizationService
    {
        bool Authorize(Customer customer, ResourceOperation operation);
        bool CanAddToFavorites(Customer customer);
    }
}
