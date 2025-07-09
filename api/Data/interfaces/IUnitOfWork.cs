using api.Repositories.interfaces;
using api.Repository.interfaces;
using api.Repository.Projects.interfaces;
using api.Repository.Users.interfaces;
using api.Repository.Workspaces.interfaces;

namespace api.Data.interfaces;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    IWorkSpaceRepository WorkSpaces {get; }
    IProjectRepository Projects { get; }
    ITaskRepository Tasks { get;  }


    INotificationRepository Notifications{get; }
    Task CompleteAsync();
}
