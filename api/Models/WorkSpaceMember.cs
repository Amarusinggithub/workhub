using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Enums;
namespace api.Models;

public class WorkSpaceMember
{
    [Key]
    public int Id { get; set; }

    [Required]
    public Guid WorkSpaceId { get; set; }
    public Workspace Workspace { get; set; }

    [Required]
    public Guid UserId { get; set; }
    public User User { get; set; }

    [Required]
    public Guid RoleId { get; set; }
    public WorkspaceRole WorkspaceRole { get; set; }

    public WorkspaceMemberStatus Status { get; set; } = WorkspaceMemberStatus.Active;

    public DateTime AddedAt { get; set; } = DateTime.UtcNow;

    public DateTime? RemovedAt { get; set; }

    public Guid? AddedByUserId { get; set; }

    [ForeignKey("AddedByUserId")]

    public User? AddedBy { get; set; }



    public Guid? RemovedByUserId { get; set; }

    [ForeignKey("RemovedByUserId")]

    public User? RemovedBy { get; set; }

    [MaxLength(500)]
    public string? RemovalReason { get; set; }

    public DateTime? LastAccessAt { get; set; }

    public bool ReceiveNotifications { get; set; } = true;

    [MaxLength(100)]
    public string? Title { get; set; }

    [MaxLength(500)]
    public string? Notes { get; set; }

    public bool IsActive => RemovedAt == null && Status == WorkspaceMemberStatus.Active;

    public bool IsOwner => WorkspaceRole.Role?.Name.ToLower() == "owner";

    public bool IsAdmin => WorkspaceRole.Role?.Name.ToLower() == "admin" || IsOwner;
}
