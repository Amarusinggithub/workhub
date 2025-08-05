using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Enums;
using Microsoft.EntityFrameworkCore;

namespace api.Models;

public class WorkspaceMemberShip
{
    [Key]
    public int Id { get; set; }

    [Required]
    public Guid WorkSpaceId { get; set; }
    [ForeignKey(nameof(WorkSpaceId))]

    public Workspace Workspace { get; set; }

    [Required]
    public Guid UserId { get; set; }
    [ForeignKey(nameof(UserId))]


    public User User { get; set; }

    [Required]
    public int WorkspaceRoleId { get; set; }
    [ForeignKey(nameof(WorkspaceRoleId))]

    public WorkspaceRole WorkspaceRole { get; set; }

    public WorkspaceMemberStatus Status { get; set; } = WorkspaceMemberStatus.Active;
    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.UtcNow;

    public DateTime AddedAt { get; set; } = DateTime.UtcNow;

    public DateTime? RemovedAt { get; set; }

    public Guid? AddedByUserId { get; set; }

    [ForeignKey(nameof(AddedByUserId))]

    public User? AddedBy { get; set; }



    public Guid? RemovedByUserId { get; set; }

    [ForeignKey(nameof(RemovedByUserId))]

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


    public static void ConfigureRelations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WorkspaceMemberShip>()
            .HasOne(iws => iws.Workspace)
            .WithMany(ws => ws.WorkspaceMemberships)
            .HasForeignKey(iws => iws.WorkSpaceId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WorkspaceMemberShip>()
            .HasOne(iws => iws.User)
            .WithMany(u => u.WorkspaceMemberShips)
            .HasForeignKey(iws => iws.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WorkspaceMemberShip>()
            .HasOne(iws => iws.AddedBy)
            .WithMany(u => u.CreatedWorkspaceMemberShips)
            .HasForeignKey(iws => iws.AddedByUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
