using api.Data;
using api.Repository.Workspaces.interfaces;

namespace api.Repository.Workspaces;

public class WorkSpaceSettingsRepository(ApplicationDbContext context, ILogger logger) :IWorkSpaceSettingsRepository
{

}
