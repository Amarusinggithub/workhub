using api.Data;
using api.Repository.interfaces;
using api.Repositorys.interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class GenericRepository<T>: IGenericRepository<T> where T:class
{
    protected ApplicationDbContext _context;
    protected DbSet<T> dbSet;
    protected readonly ILogger _logger;

    public GenericRepository(ApplicationDbContext context,ILogger logger)
    {
        _context = context;
        _logger = logger;
        dbSet = context.Set<T>();
    }
    public virtual async Task<T> GetById(int id)
    {
        return await dbSet.FindAsync(id);
    }

    public virtual async Task<IEnumerable<T>> GetAll()
    {
        return await dbSet.ToListAsync();
    }

    public virtual async Task<bool> Add(T entity)
    {
        await dbSet.AddAsync(entity);
        return true;
    }

    public virtual Task<bool> Update(T entity)
    {
        throw new NotImplementedException();
    }

    public virtual Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public virtual Task<bool> Upsert(T entity)
    {
        throw new NotImplementedException();
    }
}
