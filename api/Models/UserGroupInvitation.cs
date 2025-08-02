using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Enums;

namespace api.Models;

public class UserGroupInvitation
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public int UserGroupId { get; set; }
    public UserGroup UserGroup { get; set; }

    [Required]
    [MaxLength(200)]
    public string Email { get; set; } = string.Empty;

    public UserGroupMemberRole Role { get; set; } = UserGroupMemberRole.Member;

    [Required]
    public Guid InvitedByUserId { get; set; }

    [ForeignKey("InvitedByUserId")]

    public User InvitedBy { get; set; }

    public Guid? InvitedUserId { get; set; }

    [ForeignKey("InvitedUserId")]

    public User? InvitedUser { get; set; }

    public InvitationStatus Status { get; set; } = InvitationStatus.Pending;

    [MaxLength(500)]
    public string? Message { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime ExpiresAt { get; set; }

    public DateTime? AcceptedAt { get; set; }

    public DateTime? DeclinedAt { get; set; }

    [MaxLength(500)]
    public string? DeclineReason { get; set; }

    public bool IsExpired => DateTime.UtcNow > ExpiresAt;

    public bool IsValid => Status == InvitationStatus.Pending && !IsExpired;
}

