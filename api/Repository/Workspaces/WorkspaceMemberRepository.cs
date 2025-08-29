using api.Data;
using api.Repository.Workspaces.interfaces;

namespace api.Repository.Workspaces;

public class WorkSpaceMemberRepository(ApplicationDbContext context, ILogger logger) :IWorkSpaceMemberRepository
{

}
