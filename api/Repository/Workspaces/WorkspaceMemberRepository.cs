using api.Data;
using api.Repository.Workspaces.interfaces;

namespace api.Repository.Workspaces;

public class WorkspaceMemberRepository(ApplicationDbContext context, ILogger logger) :IWorkspaceMemberRepository
{

}
