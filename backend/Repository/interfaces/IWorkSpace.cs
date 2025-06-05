using backend.Models;

namespace backend.Repositorys.interfaces;

public interface IWorkSpace
{
    public abstract WorkSpace GetWorkSpaceById(int id);
    public abstract IEnumerable<WorkSpace> GetAll();
    public abstract void Add(User entity);
    public abstract void Update(User entity);
    public abstract void Delete(int id);

}
