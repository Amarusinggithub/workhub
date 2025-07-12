using api.Data;
using api.Repository.Workspaces.interfaces;

namespace api.Repository.Workspaces;

public class WorkSpaceRoleRepository(ApplicationDbContext context, ILogger logger) :IWorkSpaceRoleRepository
{

}
