using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Enums;
using Microsoft.EntityFrameworkCore;

namespace api.Models;

public class GroupInvitation
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid UserGroupId { get; set; }
    [ForeignKey(nameof(UserGroupId))]

    public UserGroup UserGroup { get; set; }

    [Required]
    [MaxLength(200)]
    public string Email { get; set; } = string.Empty;

    public UserGroupMemberRole Role { get; set; } = UserGroupMemberRole.Member;

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



        modelBuilder.Entity<GroupInvitation>()
            .HasOne(iug => iug.UserGroup)
            .WithMany(ug => ug.Invitations)
            .HasForeignKey(iug => iug.UserGroupId)
            .OnDelete(DeleteBehavior.Cascade);



        modelBuilder.Entity<GroupInvitation>()
            .HasOne(iug => iug.InvitedUser)
            .WithMany(ug => ug.GroupInvitationsReceived)
            .HasForeignKey(iug => iug.InvitedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

