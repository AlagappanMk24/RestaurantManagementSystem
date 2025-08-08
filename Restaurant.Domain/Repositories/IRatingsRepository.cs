using Restaurant.Domain.Constants;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories.GenericRepository;

namespace Restaurant.Domain.Repositories;

public interface IRatingsRepository : IGenericRepository<Rating>
{
    Task<(IEnumerable<Rating> Ratings, int TotalCount)> GetAllMatchingAsync(
    string? searchPhrase,
    int pageSize,
    int pageNumber,
    string? sortBy,
    SortDirection sortDirection);
    Task<Rating?> GetByIdWithIncluded(int id);
}
