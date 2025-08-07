using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Repositories.GenericRepository;
using Restaurant.Infrastructure.Data.Context;

namespace Restaurant.Infrastructure.Repositories.GenericRepository;

public class GenericRepository<T>(RestaurantDbContext dbContext) : IGenericRepository<T> where T : class
{
    private DbSet<T> Set => dbContext.Set<T>();

    public async Task<IEnumerable<T>> GetAllAsync()
        => await Set.ToListAsync();

    public async Task<T?> GetByIdAsync(int id)
        => await Set.FindAsync(id);

    public async Task<T?> GetByNameAsync(string name)
   => await Set.FirstOrDefaultAsync(
           e => EF.Property<string>(e, "Name").Contains(name));

    public async Task AddAsync(T entity)
    {
        await Set.AddAsync(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, T entity)
    {
        dbContext.Entry(entity).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        Set.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public Task SaveChanges()
     => dbContext.SaveChangesAsync();

    public IQueryable<T> Query()
     => dbContext.Set<T>().AsQueryable();
}
