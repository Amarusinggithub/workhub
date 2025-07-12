using api.Data;
using api.Repository.Projects.interfaces;

namespace api.Repository.Projects;

public class ProjectSettingsRepository(ApplicationDbContext context, ILogger logger) :IProjectSettingsRepository
{

}
