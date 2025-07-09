using api.Models;
using api.Repository.interfaces;
using Task = api.Models.Task;

namespace api.Repositories.interfaces;

public interface ITaskRepository: IGenericRepository<Task>
{

}
