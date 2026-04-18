using api.Data;
using api.Repository.interfaces;
using api.Repositories.interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class GenericRepository<T, TId> : IGenericRepository<T, TId> where T : class
{
    protected ApplicationDbContext _context;
    protected DbSet<T> dbSet;
    protected readonly ILogger _logger;

    public GenericRepository(ApplicationDbContext context, ILogger logger)
    {
        _context = context;
        _logger = logger;
        dbSet = context.Set<T>();
    }

    public virtual async Task<T?> GetById(TId id)
    {
        return await dbSet.FindAsync(id);
    }

    public virtual async Task<IEnumerable<T>> GetAll(int page = 1, int pageSize = 50)
    {
        return await dbSet
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public virtual async Task<bool> Add(T entity)
    {
        await dbSet.AddAsync(entity);
        return true;
    }

    public virtual Task<bool> Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        return Task.FromResult(true);
    }

    public virtual async Task<bool> Delete(TId id)
    {
        var entity = await dbSet.FindAsync(id);
        if (entity == null) return false;
        dbSet.Remove(entity);
        return true;
    }

    public virtual async Task<bool> Upsert(T entity)
    {
        var entry = _context.Entry(entity);
        if (entry.IsKeySet)
        {
            entry.State = EntityState.Modified;
        }
        else
        {
            await dbSet.AddAsync(entity);
        }
        return true;
    }
}
