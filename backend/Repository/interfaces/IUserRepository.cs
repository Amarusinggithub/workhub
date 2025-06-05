using backend.Models;

namespace backend.Repositorys.interfaces;

public interface IUserRepository
{
    public abstract User GetUserById(int id);
    public abstract IEnumerable<User> GetAll();
    public abstract void Add(User entity);
    public abstract void Update(User entity);
    public abstract void Delete(int id);
}
