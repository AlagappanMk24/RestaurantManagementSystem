using Restaurant.Domain.Constants;
using Restaurant.Domain.Repositories.GenericRepository;

namespace Restaurant.Domain.Repositories;
public interface IRestaurantRepository : IGenericRepository<Domain.Entities.RestaurantEntity>
{
    Task<(IEnumerable<Domain.Entities.RestaurantEntity>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection);
    Task<List<string>> GetCategoriesForRestaurantAsync(int restaurantId);
    Task<List<Domain.Entities.RestaurantEntity>> GetTopRatedAsync(int count);
    Task<Domain.Entities.RestaurantEntity?> GetStatisticsAsync(int restaurantId);
    Task<int> GetTotalOrdersForRestaurantAsync(int restaurantId);
    Task<decimal> GetTotalRevenueForRestaurantAsync(int restaurantId);
    Task<string?> GetMostPopularDishNameAsync(int restaurantId);
    Task<DateTime?> GetLastOrderDateAsync(int restaurantId);
}
