using api.Repositorys.interfaces;

namespace api.Repository.interfaces;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    IWorkSpaceRepository WorkSpaces {get; }
    IProjectRepository Projects { get; }
    ITaskRepository Tasks { get;  }

    INotificationRepository Notifications{get; }
    Task CompleteAsync();
}
