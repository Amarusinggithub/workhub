namespace api.Repository.interfaces;

public interface IGenericRepository<T, TId> where T : class
{
    Task<T?> GetById(TId id);
    Task<IEnumerable<T>> GetAll(int page = 1, int pageSize = 50);
    Task<bool> Add(T entity);
    Task<bool> Update(T entity);
    Task<bool> Delete(TId id);
    Task<bool> Upsert(T entity);
}
