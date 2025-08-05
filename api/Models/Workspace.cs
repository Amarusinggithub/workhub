using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Enums;
using api.Models;
using Microsoft.EntityFrameworkCore;

public class Workspace
{
    [Key] public Guid Id { get; set; } =  Guid.NewGuid();

    [Required]
    [MaxLength(100)]
    public string WorkSpaceName { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }

    [MaxLength(50)]
    public string? WorkspaceCode { get; set; }

    [Required]
    public Guid OwnerId { get; set; }
    [ForeignKey(nameof(OwnerId))]

    public User Owner { get; set; }

    [Required]
    public Guid UserGroupId { get; set; }
    [ForeignKey(nameof(UserGroupId))]


    public UserGroup UserGroup { get; set; }

    public WorkspaceVisibility Visibility { get; set; } = WorkspaceVisibility.Private;

    public WorkspaceStatus Status { get; set; } = WorkspaceStatus.Active;

    public bool IsPersonal { get; set; } = false;

    public int MaxMembers { get; set; } = 10;

    public int MaxProjects { get; set; } = 5;

    [Column(TypeName = "bigint")]
    public long MaxStorageBytes { get; set; } = 1_000_000_000; // 1GB default

    [Column(TypeName = "bigint")]
    public long CurrentStorageBytes { get; set; } = 0;

    public bool AllowExternalInvites { get; set; } = true;

    public bool RequireApprovalForJoining { get; set; } = false;

    [MaxLength(100)]
    public string? TimeZone { get; set; }

    [MaxLength(10)]
    public string? Language { get; set; } = "en";

    [Column(TypeName = "jsonb")]
    public string? Settings { get; set; }

    public DateTime? LastActivityAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
       public DateTime CreatedAt { get; set; }= DateTime.UtcNow;


    public DateTime? DeletedAt { get; set; }

    public Guid? ModifiedByUserId { get; set; }

    public int ProjectCount => Projects?.Count(p => p.DeletedAt == null) ?? 0;

    public int MemberCount => WorkspaceMemberships?.Count(m => m.RemovedAt == null) ?? 0;

    public bool IsDeleted => DeletedAt.HasValue;

    public bool IsActive => Status == WorkspaceStatus.Active && !IsDeleted;

    public ICollection<Project> Projects { get; set; } = new List<Project>();
    public ICollection<WorkspaceRole> WorkspaceRoles { get; set; } = new List<WorkspaceRole>();
    public ICollection<WorkspaceMemberShip> WorkspaceMemberships { get; set; } = new List<WorkspaceMemberShip>();
    public ICollection<ProjectAttachment> ProjectResources { get; set; } = new List<ProjectAttachment>();
    public ICollection<WorkspaceInvitation> Invitations { get; set; } = new List<WorkspaceInvitation>();


    public static void ConfigureRelations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Workspace>()
            .HasOne(ugr => ugr.Owner)
            .WithMany(u => u.CreatedWorkspaces)
            .HasForeignKey(ugr => ugr.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
