using api.Models;
using api.Repository.interfaces;

namespace api.Repositories.interfaces;

public interface ITaskRepository: IGenericRepository<TaskItem,Guid>
{

}
