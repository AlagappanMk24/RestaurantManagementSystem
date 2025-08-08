using Restaurant.Domain.Constants;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories.GenericRepository;

namespace Restaurant.Domain.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<(IEnumerable<Order>, int)> GetAllMatchingAsync(int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection);
        Task<Order?> GetByIdIncludeWithOrderItemsAsync(int id);
    }
}
