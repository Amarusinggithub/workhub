using System.ComponentModel.DataAnnotations;
using api.Enums;

namespace api.Models;

public class Project
{

    [Key]
    public Guid  Id { get; set; }

    [Required]
    public string ProjectName { get; set; } = string.Empty;

    public string? ProjectDescription { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public DateTime? ScheduledFinishAt { get; set; }
    public DateTime? FinishedAt { get; set; }
    public DateTime? ArchivedAt { get; set; }
    public DateTime? ScheduledDeleteAt { get; set; }

    public bool IsArchived { get; set; } = false;
    public bool IsTrashed { get; set; } = false;
    public bool IsDeleted => DeletedAt.HasValue;
    public ProjectStatus Status { get; set; } = ProjectStatus.Active;

    public DateTime? TrashedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public DateTime? LastActivityAt { get; set; }
    public Guid? ModifiedByUserId { get; set; }
    public int MemberCount => ProjectMembers?.Count(m => m.RemovedAt == null) ?? 0;

    public bool IsActive => Status == ProjectStatus.Active && !IsDeleted;


    [Required]
    public Guid  WorkSpaceId { get; set; }
    public Workspace Workspace { get; set; }

    public ICollection<WorkspaceRole> UserProjectRoles { get; set; } = new List<WorkspaceRole>();


    public ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();
    public ICollection<ProjectCategory> ProjectCategories { get; set; } = new List<ProjectCategory>();

    public ICollection<TaskItem> ProjectTasks { get; set; } = new List<TaskItem>();

    public ICollection<ProjectResource> ProjectResources { get; set; } = new List<ProjectResource>();

}
