using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Enums;
using Microsoft.EntityFrameworkCore;

namespace api.Models;

public class WorkspaceInvitation
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid WorkspaceId { get; set; }
    [ForeignKey(nameof(WorkspaceId))]

    public Workspace Workspace { get; set; }

    [Required]
    [MaxLength(200)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public int WorkspaceRoleId { get; set; }
    [ForeignKey(nameof(WorkspaceRoleId))]

    public WorkspaceRole WorkspaceRole { get; set; }

    [Required]
    public Guid InvitedByUserId { get; set; }

    [ForeignKey(nameof(InvitedByUserId))]

    public User InvitedBy { get; set; }


    public Guid? InvitedUserId { get; set; }

    [ForeignKey(nameof(InvitedUserId))]

    public User? InvitedUser { get; set; }

    public InvitationStatus Status { get; set; } = InvitationStatus.Pending;

    [MaxLength(500)]
    public string? Message { get; set; }

    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.UtcNow;
    public DateTime ExpiresAt { get; set; }

    public DateTime? AcceptedAt { get; set; }

    public DateTime? DeclinedAt { get; set; }

    [MaxLength(500)]
    public string? DeclineReason { get; set; }

    public bool IsExpired => DateTime.UtcNow > ExpiresAt;

    public bool IsValid => Status == InvitationStatus.Pending && !IsExpired;

    public static void ConfigureRelations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WorkspaceInvitation>()
            .HasOne(wi => wi.InvitedBy)
            .WithMany(u => u.WorkspaceInvitationsSent)
            .HasForeignKey(wi => wi.InvitedByUserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WorkspaceInvitation>()
            .HasOne(wi => wi.InvitedUser)
            .WithMany(u => u.WorkspaceInvitationsReceived)
            .HasForeignKey(wi => wi.InvitedUserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WorkspaceInvitation>()
            .HasOne(iug => iug.Workspace)
            .WithMany(ug => ug.Invitations)
            .HasForeignKey(iug => iug.WorkspaceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
