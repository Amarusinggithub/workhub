using api.Data;
using api.Repository.Tasks.interfaces;

namespace api.Repository.Tasks;

public class SubtaskRepository(ApplicationDbContext context, ILogger logger) :ISubtaskRepository
{

}
