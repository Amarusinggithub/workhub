using api.Data;
using api.Repository.Projects.interfaces;

namespace api.Repository.Projects;

public class ProjectMemberRepository(ApplicationDbContext context, ILogger logger) :IProjectMemberRepository
{

}
