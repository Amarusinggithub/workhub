using System.ComponentModel.DataAnnotations;
using api.Enums;

namespace api.Models;

public class UserGroupMember
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UserGroupId { get; set; }
    public UserGroup UserGroup { get; set; }

    [Required]
    public Guid UserId { get; set; }
    public User User { get; set; }

    public UserGroupMemberRole Role { get; set; } = UserGroupMemberRole.Member;

    public UserGroupMemberStatus Status { get; set; } = UserGroupMemberStatus.Active;

    public DateTime AddedAt { get; set; } = DateTime.UtcNow;

    public DateTime? RemovedAt { get; set; }

    public Guid? AddedByUserId { get; set; }
    public User? AddedBy { get; set; }

    public Guid? RemovedByUserId { get; set; }
    public User? RemovedBy { get; set; }

    [MaxLength(500)]
    public string? RemovalReason { get; set; }

    public DateTime? LastAccessAt { get; set; }

    [MaxLength(100)]
    public string? Title { get; set; }

    [MaxLength(500)]
    public string? Notes { get; set; }

    public bool ReceiveBillingNotifications { get; set; } = false;

    public bool ReceiveUsageNotifications { get; set; } = false;

    public bool IsActive => RemovedAt == null && Status == UserGroupMemberStatus.Active;

    public bool IsOwner => Role == UserGroupMemberRole.Owner;

    public bool IsAdmin => Role == UserGroupMemberRole.Admin || IsOwner;

    public bool CanManageBilling => IsAdmin && ReceiveBillingNotifications || IsOwner;
}
