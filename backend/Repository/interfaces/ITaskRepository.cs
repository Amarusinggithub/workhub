using Task = backend.Models.Task;

namespace backend.Repositorys.interfaces;

public interface ITaskRepository
{
    public abstract Task GetTaskById(int id);
    public abstract IEnumerable<Task> GetAll();
    public abstract void Add(Task entity);
    public abstract void Update(Task entity);
    public abstract void Delete(int id);
}
