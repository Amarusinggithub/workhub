using api.Data;
using api.Repository.Workspaces.interfaces;

namespace api.Repository.Workspaces;

public class WorkspaceInviteRepository(ApplicationDbContext context, ILogger logger) :IWorkspaceInviteRepository
{

}
