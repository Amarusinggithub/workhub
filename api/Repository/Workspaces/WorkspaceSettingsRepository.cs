using api.Data;
using api.Repository.Workspaces.interfaces;

namespace api.Repository.Workspaces;

public class WorkspaceSettingsRepository(ApplicationDbContext context, ILogger logger) :IWorkspaceSettingsRepository
{

}
