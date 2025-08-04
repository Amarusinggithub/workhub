using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Enums;
using Microsoft.EntityFrameworkCore;

namespace api.Models;

public class GroupMembership
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UserGroupId { get; set; }
    [ForeignKey(nameof(UserGroupId))]

    public UserGroup UserGroup { get; set; }

    [Required]
    public Guid UserId { get; set; }
    [ForeignKey(nameof(UserId))]

    public User User { get; set; }

    public UserGroupMemberRole Role { get; set; } = UserGroupMemberRole.Member;

    public UserGroupMemberStatus Status { get; set; } = UserGroupMemberStatus.Active;

    public DateTime AddedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.UtcNow;

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


    public static void ConfigureRelations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GroupMembership>()
            .HasOne(iug => iug.UserGroup)
            .WithMany(ug => ug.Users)
            .HasForeignKey(iug => iug.UserGroupId)
            .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<GroupMembership>()
            .HasOne(iug => iug.UserGroup)
            .WithMany(ug => ug.Users)
            .HasForeignKey(iug => iug.UserGroupId)
            .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<GroupMembership>()
            .HasOne(iug => iug.UserGroup)
            .WithMany(ug => ug.Users)
            .HasForeignKey(iug => iug.UserGroupId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<GroupMembership>()
            .HasOne(iug => iug.User)
            .WithMany(u => u.GroupMemberships)
            .HasForeignKey(iug => iug.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
