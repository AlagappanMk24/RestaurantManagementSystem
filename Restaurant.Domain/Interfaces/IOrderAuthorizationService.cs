using Restaurant.Domain.Entities;

namespace Restaurant.Domain.Interfaces
{
    public interface IOrderAuthorizationService
    {
        bool CanViewCustomerOrder(string customerApplicationUserId);
        bool CanViewRestaurantOrders(string ownerId);
        bool CanModifyOrder(Order order);
        bool CanViewAllOrders();
    }
}