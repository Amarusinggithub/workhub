namespace api.Repository.interfaces;

public interface IGenericRepository <T, TId> where T : class
{
    Task<T> GetById(TId id);
   Task<IEnumerable<T>>  GetAll();
    Task<bool> Add(T entity);
    Task<bool> Update(T entity);
    Task<bool> Delete(TId id);
}
