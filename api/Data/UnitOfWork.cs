using api.Data.interfaces;
using api.Repository;
using api.Repository.interfaces;
using api.Repositories.interfaces;
using api.Repository.Analytics;
using api.Repository.Analytics.interfaces;
using api.Repository.Boards;
using api.Repository.Boards.interfaces;
using api.Repository.Infrastructure;
using api.Repository.Infrastructure.interfaces;
using api.Repository.Notifications;
using api.Repository.Notifications.interfaces;
using api.Repository.Projects;
using api.Repository.Projects.interfaces;
using api.Repository.Software;
using api.Repository.Subscription;
using api.Repository.Subscription.interfaces;
using api.Repository.Tasks;
using api.Repository.Tasks.interfaces;
using api.Repository.Users;
using api.Repository.Users.interfaces;
using api.Repository.Workspaces;
using api.Repository.Workspaces.interfaces;

namespace api.Data;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ILogger _logger;
    private readonly ApplicationDbContext _context;

    public IUserRepository Users { get; private set; }
    public IWorkSpaceRepository Workspaces { get; private set; }
    public IProjectRepository Projects { get; private set; }
    public ITaskRepository Tasks { get; private set; }
    public IActivityLogRepository ActivityLogs { get; private set; }
    public IAuditLogRepository AuditLogs { get; private set; }
    public IReportRepository Reports { get; private set; }
    public IDashboardRepository Dashboards { get; private set; }
    public IBoardRepository Boards { get; private set; }
    public IBoardColumnRepository BoardColumns { get; private set; }
    public IBoardFilterRepository BoardFilters { get; private set; }
    public IBoardViewRepository BoardViews { get; private set; }
    public ICacheRepository Caches { get; private set; }
    public ISettingRepository Settings { get; private set; }
    public IStorageRepository Storages { get; private set; }
    public IWebhookRepository Webhooks { get; private set; }
    public IEmailLogRepository EmailLogs { get; private set; }
    public IUserNotificationRepository UserNotifications { get; private set; }
    public IProjectMemberRepository ProjectMembers { get; private set; }
    public IProjectSettingsRepository ProjectSettings { get; private set; }
    public ICustomerRepository Customers { get; private set; }
    public IInvoiceRepository Invoices { get; private set; }
    public ISubscriptionRepository Subscriptions { get; private set; }
    public IUsageRecordRepository UsageRecords { get; private set; }
    public ISubtaskRepository Subtasks { get; private set; }
    public ITaskAttachmentRepository TaskAttachments { get; private set; }
    public ITaskCommentRepository TaskComments { get; private set; }
    public ITaskHistoryRepository TaskHistories { get; private set; }
    public ITaskLabelRepository TaskLabels { get; private set; }
    public ITaskStatusRepository TaskStatuses { get; private set; }
    public INotificationRepository Notifications { get; private set; }
    public IWorkSpaceInviteRepository WorkSpaceInvites { get; private set; }
    public IWorkSpaceMemberRepository WorkSpaceMembers { get; private set; }
    public IWorkSpaceRoleRepository WorkSpaceRoles { get; private set; }
    public IWorkSpaceSettingsRepository WorkSpaceSettings { get; private set; }

    public UnitOfWork(ApplicationDbContext context, ILoggerFactory loggerFactory)
    {
        _context = context;
        _logger = loggerFactory.CreateLogger("logs");

        Users = new UserRepository(_context, _logger);
        Workspaces = new WorkSpaceRepository(_context, _logger);
        Projects = new ProjectRepository(_context, _logger);
        Tasks = new TaskRepository(_context, _logger);
        ActivityLogs = new ActivityLogRepository(_context, _logger);
        AuditLogs = new AuditLogRepository(_context, _logger);
        Reports = new ReportRepository(_context, _logger);
        Dashboards = new DashboardRepository(_context, _logger);
        Boards = new BoardRepository(_context, _logger);
        BoardColumns = new BoardColumnRepository(_context, _logger);
        BoardFilters = new BoardFilterRepository(_context, _logger);
        BoardViews = new BoardViewRepository(_context, _logger);
        Caches = new CacheRepository(_context, _logger);
        Settings = new SettingRepository(_context, _logger);
        Storages = new StorageRepository(_context, _logger);
        Webhooks = new WebhookRepository(_context, _logger);
        EmailLogs = new EmailLogRepository(_context, _logger);
        UserNotifications = new UserNotificationRepository(_context, _logger);
        ProjectMembers = new ProjectMemberRepository(_context, _logger);
        ProjectSettings = new ProjectSettingsRepository(_context, _logger);
        Customers = new CustomerRepository(_context, _logger);
        Invoices = new InvoiceRepository(_context, _logger);
        Subscriptions = new SubscriptionRepository(_context, _logger);
        UsageRecords = new UsageRecordRepository(_context, _logger);
        Subtasks = new SubtaskRepository(_context, _logger);
        TaskAttachments = new TaskAttachmentRepository(_context, _logger);
        TaskComments = new TaskCommentRepository(_context, _logger);
        TaskHistories = new TaskHistoryRepository(_context, _logger);
        TaskLabels = new TaskLabelRepository(_context, _logger);
        TaskStatuses = new TaskStatusRepository(_context, _logger);
        Notifications = new NotificationRepository(_context, _logger);
        WorkSpaceInvites = new WorkSpaceInviteRepository(_context, _logger);
        WorkSpaceMembers = new WorkSpaceMemberRepository(_context, _logger);
        WorkSpaceRoles = new WorkSpaceRoleRepository(_context, _logger);
        WorkSpaceSettings = new WorkSpaceSettingsRepository(_context, _logger);
    }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
