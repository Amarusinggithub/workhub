using api.Data;
using api.Repository.Tasks.interfaces;

namespace api.Repository.Tasks;

public class TaskCommentRepository(ApplicationDbContext context, ILogger logger) :ITaskCommentRepository
{

}
