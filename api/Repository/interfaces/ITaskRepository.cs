using api.Models;
using api.Repository.interfaces;
using Task = api.Models.Task;

namespace api.Repositorys.interfaces;

public interface ITaskRepository: IGenericRepository<Task>
{

}
