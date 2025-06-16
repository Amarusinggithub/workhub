using api.Repository.interfaces;
using Models_Task = api.Models.Task;
using Task = api.Models.Task;

namespace api.Repositorys.interfaces;

public interface ITaskRepository: IGenericRepository<Models_Task>
{

}
