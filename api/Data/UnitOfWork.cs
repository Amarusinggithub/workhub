using api.Data.interfaces;
using api.Repository;
using api.Repository.interfaces;
using api.Repositories.interfaces;

namespace api.Data;

public class UnitOfWork:IUnitOfWork,IDisposable
{
    private readonly ILogger _logger;
    private readonly ApplicationDbContext _context;
    public IUserRepository Users { get; private set; }
    public IWorkSpaceRepository WorkSpaces { get; }
    public IProjectRepository Projects { get; }
    public ITaskRepository Tasks { get; private set; }
    public INotificationRepository Notifications { get; }

    public UnitOfWork(ApplicationDbContext context,ILoggerFactory loggerFactory)
    {
        _context = context;
        _logger = loggerFactory.CreateLogger("logs");
        Users = new UserRepository(_context, _logger);
        WorkSpaces = new WorkSpaceRepository(_context, _logger);
        Projects = new ProjectRepository(_context, _logger);
        Tasks = new TaskRepository(_context, _logger);
        Notifications= new NotificationRepository(_context, _logger);

    }

    public async Task CompleteAsync()
    {
         await _context.SaveChangesAsync();
    }

    /*public async void Dispose()
    {
       await _context.DisposeAsync();
    }*/

    public void  Dispose()
    {
         _context.Dispose();
    }
}
