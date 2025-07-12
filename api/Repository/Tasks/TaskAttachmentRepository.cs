using api.Data;
using api.Repository.Tasks.interfaces;

namespace api.Repository.Tasks;

public class TaskAttachmentRepository(ApplicationDbContext context, ILogger logger) :ITaskAttachmentRepository
{

}
