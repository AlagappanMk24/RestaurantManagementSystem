using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Constants;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;
using Restaurant.Infrastructure.Data.Context;
using Restaurant.Infrastructure.Repositories.GenericRepository;
using System.Linq.Expressions;

namespace Restaurant.Infrastructure.Repositories;

public class CategoryRepository(RestaurantDbContext dbContext) : GenericRepository<Category>(dbContext), ICategoryRepository
{
    public async Task<(IEnumerable<Category>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection)
    {
        var searchPhraseLower = searchPhrase?.ToLower();

        var baseQuery = dbContext
            .Categories.
            Include(c => c.Dishes)
            .ThenInclude(d => d.Restaurant)
            .AsNoTracking()
            .Where(d => searchPhraseLower == null || d.Name.ToLower().Contains(searchPhraseLower)
                                                   || d.Description!.ToLower().Contains(searchPhraseLower));

        var totalCount = await baseQuery.CountAsync();

        if (sortBy != null)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Category, object>>>
            {
                { nameof(Category.Name), d => d.Name },
                { nameof(Category.Description), d => d.Description! },
            };

            var selectedColumn = columnsSelector[sortBy];

            baseQuery = sortDirection == SortDirection.Ascending
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }

        var categories = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .AsNoTracking()
        .ToListAsync();

        return (categories, totalCount);
    }

    public async Task<Category?> GetByIdWithDishesAndRestaurantsAsync(int id)
    {
        return await dbContext.Categories
            .Include(c => c.Dishes)
                .ThenInclude(d => d.Restaurant)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

}
