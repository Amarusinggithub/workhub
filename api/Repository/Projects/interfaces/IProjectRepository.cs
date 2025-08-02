using api.Models;
using api.Repository.interfaces;

namespace api.Repository.Projects.interfaces;

public interface IProjectRepository: IGenericRepository<Project,Guid>
{

}
