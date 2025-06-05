using backend.Models;

namespace backend.Repositorys.interfaces;

public interface INotificationRepository
{
    public abstract Notification GetNotificationById(int id);
    public abstract IEnumerable<Notification> GetAll();
    public abstract void Add(Notification entity);
    public abstract void Update(Notification entity);
    public abstract void Delete(int id);
}
