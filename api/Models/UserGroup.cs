using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Enums;

namespace api.Models;

public class UserGroup
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string GroupName { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }

    [Required]
    public int UserGroupTypeId { get; set; }
    [ForeignKey(nameof(UserGroupTypeId))]

    public UserGroupType UserGroupType { get; set; }

    [Required]
    public Guid OwnerId { get; set; }
    [ForeignKey(nameof(OwnerId))]

    public User Owner { get; set; }

    public UserGroupStatus Status { get; set; } = UserGroupStatus.Active;

    [MaxLength(100)]
    public string? CompanyName { get; set; }

    [MaxLength(200)]
    public string? Website { get; set; }

    [MaxLength(100)]
    public string? Industry { get; set; }

    [MaxLength(100)]
    public string? Country { get; set; }

    [MaxLength(100)]
    public string? TimeZone { get; set; }

    [MaxLength(10)]
    public string? Currency { get; set; } = "USD";

    [Column(TypeName = "jsonb")]
    public string? Settings { get; set; }

    [Column(TypeName = "bigint")]
    public long CurrentStorageBytes { get; set; } = 0;

    public DateTime? LastActivityAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Guid? DeletedByUserId { get; set; }

    [MaxLength(500)]
    public string? DeletionReason { get; set; }

    // Computed Properties
    public int MemberCount => Users?.Count(u => u.IsActive) ?? 0;

    public int WorkspaceCount => Workspaces?.Count(w => w.DeletedAt == null) ?? 0;

    public int AdminCount => Users?.Count(u => u.IsActive && u.IsAdmin) ?? 0;

    public bool IsDeleted => DeletedAt.HasValue;

    public bool IsActive => Status == UserGroupStatus.Active && !IsDeleted;

    public long MaxStorageBytes => UserGroupType?.MaxStorageBytes ?? 0;

    public double StorageUsagePercentage => MaxStorageBytes > 0 ?
        (double)CurrentStorageBytes / MaxStorageBytes * 100 : 0;

    public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    public ICollection<GroupMemberShip> Users { get; set; } = new List<GroupMemberShip>();
    public ICollection<Workspace> Workspaces { get; set; } = new List<Workspace>();
    public ICollection<GroupInvitation> Invitations { get; set; } = new List<GroupInvitation>();
}
