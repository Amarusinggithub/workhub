using api.Data;
using api.Repository.Tasks.interfaces;

namespace api.Repository.Tasks;

public class TaskLabelRepository(ApplicationDbContext context, ILogger logger) :ITaskLabelRepository
{

}
