using api.Data.interfaces;
using api.Repository.interfaces;
using api.Repositories.interfaces;
using api.Repository.Analytics.interfaces;
using api.Repository.Boards.interfaces;
using api.Repository.Infrastructure.interfaces;
using api.Repository.Notifications.interfaces;
using api.Repository.Projects.interfaces;
using api.Repository.Subscription.interfaces;
using api.Repository.Tasks.interfaces;
using api.Repository.Users.interfaces;
using api.Repository.Workspaces.interfaces;

namespace api.Data;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApplicationDbContext _context;

    public IUserRepository Users { get; }
    public IWorkspaceRepository Workspaces { get; }
    public IProjectRepository Projects { get; }
    public ITaskRepository Tasks { get; }
    public IActivityLogRepository ActivityLogs { get; }
    public IAuditLogRepository AuditLogs { get; }
    public IReportRepository Reports { get; }
    public IDashboardRepository Dashboards { get; }
    public IBoardRepository Boards { get; }
    public IBoardColumnRepository BoardColumns { get; }
    public IBoardFilterRepository BoardFilters { get; }
    public IBoardViewRepository BoardViews { get; }
    public ICacheRepository Caches { get; }
    public ISettingRepository Settings { get; }
    public IStorageRepository Storages { get; }
    public IWebhookRepository Webhooks { get; }
    public IEmailLogRepository EmailLogs { get; }
    public IUserNotificationRepository UserNotifications { get; }
    public IProjectMemberRepository ProjectMembers { get; }
    public IProjectSettingsRepository ProjectSettings { get; }
    public ICustomerRepository Customers { get; }
    public IInvoiceRepository Invoices { get; }
    public ISubscriptionRepository Subscriptions { get; }
    public IUsageRecordRepository UsageRecords { get; }
    public ISubtaskRepository Subtasks { get; }
    public ITaskAttachmentRepository TaskAttachments { get; }
    public ITaskCommentRepository TaskComments { get; }
    public ITaskHistoryRepository TaskHistories { get; }
    public ITaskLabelRepository TaskLabels { get; }
    public ITaskStatusRepository TaskStatuses { get; }
    public INotificationRepository Notifications { get; }
    public IWorkspaceInviteRepository WorkspaceInvites { get; }
    public IWorkspaceMemberRepository WorkspaceMembers { get; }
    public IWorkspaceRoleRepository WorkspaceRoles { get; }
    public IWorkspaceSettingsRepository WorkspaceSettings { get; }

    public UnitOfWork(
        ApplicationDbContext context,
        IUserRepository users,
        IWorkspaceRepository workspaces,
        IProjectRepository projects,
        ITaskRepository tasks,
        IActivityLogRepository activityLogs,
        IAuditLogRepository auditLogs,
        IReportRepository reports,
        IDashboardRepository dashboards,
        IBoardRepository boards,
        IBoardColumnRepository boardColumns,
        IBoardFilterRepository boardFilters,
        IBoardViewRepository boardViews,
        ICacheRepository caches,
        ISettingRepository settings,
        IStorageRepository storages,
        IWebhookRepository webhooks,
        IEmailLogRepository emailLogs,
        IUserNotificationRepository userNotifications,
        IProjectMemberRepository projectMembers,
        IProjectSettingsRepository projectSettings,
        ICustomerRepository customers,
        IInvoiceRepository invoices,
        ISubscriptionRepository subscriptions,
        IUsageRecordRepository usageRecords,
        ISubtaskRepository subtasks,
        ITaskAttachmentRepository taskAttachments,
        ITaskCommentRepository taskComments,
        ITaskHistoryRepository taskHistories,
        ITaskLabelRepository taskLabels,
        ITaskStatusRepository taskStatuses,
        INotificationRepository notifications,
        IWorkspaceInviteRepository workspaceInvites,
        IWorkspaceMemberRepository workspaceMembers,
        IWorkspaceRoleRepository workspaceRoles,
        IWorkspaceSettingsRepository workspaceSettings)
    {
        _context = context;
        Users = users;
        Workspaces = workspaces;
        Projects = projects;
        Tasks = tasks;
        ActivityLogs = activityLogs;
        AuditLogs = auditLogs;
        Reports = reports;
        Dashboards = dashboards;
        Boards = boards;
        BoardColumns = boardColumns;
        BoardFilters = boardFilters;
        BoardViews = boardViews;
        Caches = caches;
        Settings = settings;
        Storages = storages;
        Webhooks = webhooks;
        EmailLogs = emailLogs;
        UserNotifications = userNotifications;
        ProjectMembers = projectMembers;
        ProjectSettings = projectSettings;
        Customers = customers;
        Invoices = invoices;
        Subscriptions = subscriptions;
        UsageRecords = usageRecords;
        Subtasks = subtasks;
        TaskAttachments = taskAttachments;
        TaskComments = taskComments;
        TaskHistories = taskHistories;
        TaskLabels = taskLabels;
        TaskStatuses = taskStatuses;
        Notifications = notifications;
        WorkspaceInvites = workspaceInvites;
        WorkspaceMembers = workspaceMembers;
        WorkspaceRoles = workspaceRoles;
        WorkspaceSettings = workspaceSettings;
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
