using Restaurant.Domain.Constants;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories.GenericRepository;

namespace Restaurant.Domain.Repositories;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<(IEnumerable<Category>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection);
    Task<Category?> GetByIdWithDishesAndRestaurantsAsync(int id);
}