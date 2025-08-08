using Restaurant.Domain.Constants;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories.GenericRepository;

namespace Restaurant.Domain.Repositories;

public interface ICustomerRepository : IGenericRepository<Customer>
{
    Task<(IEnumerable<Customer>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection);
    Task<Customer?> GetByEmailAsync(string email);
    Task<Customer?> GetByPhoneNumberAsync(string phoneNumber);
    Task AddFavoriteRestaurantAsync(int customerId, int restaurantId);
    Task<List<Domain.Entities.RestaurantEntity>> GetFavoriteRestaurantsAsync(int customerId);
    Task<Customer?> GetByIdWithFavoritesAsync(int id, CancellationToken ct = default);
}
