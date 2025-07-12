using api.Data;
using api.Repository.Tasks.interfaces;

namespace api.Repository.Tasks;

public class TaskStatusRepository(ApplicationDbContext context, ILogger logger) :ITaskStatusRepository
{

}
