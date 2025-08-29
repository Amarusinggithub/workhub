using api.Data;
using api.Repository.Workspaces.interfaces;

namespace api.Repository.Workspaces;

public class WorkSpaceInviteRepository(ApplicationDbContext context, ILogger logger) :IWorkSpaceInviteRepository
{

}
