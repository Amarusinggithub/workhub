using api.Repositories.interfaces;
using api.Repository;
using api.Repository.Analytics;
using api.Repository.Analytics.interfaces;
using api.Repository.Boards;
using api.Repository.Boards.interfaces;
using api.Repository.Infrastructure;
using api.Repository.Infrastructure.interfaces;
using api.Repository.interfaces;
using api.Repository.Notifications;
using api.Repository.Notifications.interfaces;
using api.Repository.Projects;
using api.Repository.Projects.interfaces;
using api.Repository.Software;
using api.Repository.Subscription;
using api.Repository.Subscription.interfaces;
using api.Repository.Tasks;
using api.Repository.Tasks.interfaces;
using api.Repository.Users.interfaces;
using api.Repository.Workspaces.interfaces;

namespace api.Data.interfaces;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    IWorkSpaceRepository Workspaces {get; }
    IProjectRepository Projects { get; }
    ITaskRepository Tasks { get;  }
    IActivityLogRepository ActivityLogs { get;  }
    IAuditLogRepository AuditLogs { get;  }
    IReportRepository Reports { get;  }
    IDashboardRepository Dashboards { get;  }
    IBoardRepository Boards { get;  }

    IBoardColumnRepository BoardColumns { get;  }
    IBoardFilterRepository BoardFilters { get;  }

    IBoardViewRepository BoardViews { get;  }
    ICacheRepository Caches { get;  }
    ISettingRepository Settings { get;  }
    IStorageRepository Storages { get;  }
    IWebhookRepository Webhooks { get;  }
    IEmailLogRepository EmailLogs { get;  }
    IUserNotificationRepository UserNotifications { get;  }

    IProjectMemberRepository ProjectMembers  { get;  }

    IProjectSettingsRepository ProjectSettings  { get;  }

    ICustomerRepository Customers { get; }
    IInvoiceRepository Invoices { get; }
    ISubscriptionRepository Subscriptions { get; }
    IUsageRecordRepository UsageRecords { get; }
    ISubtaskRepository Subtasks { get; }
    ITaskAttachmentRepository TaskAttachments { get; }
    ITaskCommentRepository TaskComments { get; }
    ITaskHistoryRepository TaskHistories { get; }
    ITaskLabelRepository TaskLabels { get; }
    ITaskStatusRepository TaskStatuses { get; }

    INotificationRepository Notifications{get; }

    IWorkSpaceInviteRepository WorkSpaceInvites { get; }
    IWorkSpaceMemberRepository WorkSpaceMembers { get; }
    IWorkSpaceRoleRepository WorkSpaceRoles { get; }
    IWorkSpaceSettingsRepository WorkSpaceSettings { get; }



    Task CompleteAsync();
}
