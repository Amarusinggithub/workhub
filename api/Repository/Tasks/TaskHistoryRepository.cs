using api.Data;
using api.Repository.Tasks.interfaces;

namespace api.Repository.Tasks;

public class TaskHistoryRepository(ApplicationDbContext context, ILogger logger) :ITaskHistoryRepository
{

}
