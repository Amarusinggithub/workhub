using backend.Repository.interfaces;
using Task = backend.Models.Task;

namespace backend.Repositorys.interfaces;

public interface ITaskRepository: IGenericRepository<Task>
{

}
