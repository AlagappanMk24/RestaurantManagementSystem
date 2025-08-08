using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Constants;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;
using Restaurant.Infrastructure.Data.Context;
using Restaurant.Infrastructure.Repositories.GenericRepository;
using System.Linq.Expressions;

namespace Restaurant.Infrastructure.Repositories
{
    public class OrderRepository(RestaurantDbContext dbContext) : GenericRepository<Order>(dbContext), IOrderRepository
    {
        public async Task<(IEnumerable<Order>, int)> GetAllMatchingAsync(int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection)
        {
            var baseQuery = dbContext
                .Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .ThenInclude(o => o.Dish)
                .AsNoTracking();

            var totalCount = await baseQuery.CountAsync();

            if (sortBy != null)
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Order, object>>>
            {
                { nameof(Order.TotalPrice), d => d.TotalPrice },
               // { nameof(Order.Email), d => d.Email! },
            };

                if (sortBy != null && columnsSelector.TryGetValue(sortBy, out var selectedColumn))
                {
                    baseQuery = (sortDirection == SortDirection.Ascending)
                        ? baseQuery.OrderBy(selectedColumn)
                        : baseQuery.OrderByDescending(selectedColumn);
                }
            }

            var orders = await baseQuery
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
            .ToListAsync();

            return (orders, totalCount);
        }

        public async Task<Order?> GetByIdIncludeWithOrderItemsAsync(int id)
        {
            var order = await dbContext
               .Orders
               .AsNoTracking()
               .Include(o => o.Customer)
               .Include(o => o.OrderItems)
               .ThenInclude(o => o.Dish)
               .FirstOrDefaultAsync(o => o.Id == id);

            return order;
        }
    }
}
