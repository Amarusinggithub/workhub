using api.Data;
using api.Repository.Workspaces.interfaces;

namespace api.Repository.Workspaces;

public class WorkspaceRoleRepository(ApplicationDbContext context, ILogger logger) :IWorkspaceRoleRepository
{

}
