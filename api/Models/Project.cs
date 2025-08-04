using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Enums;
using Microsoft.EntityFrameworkCore;

namespace api.Models;

public class Project
{

    [Key]
    public Guid Id { get; set; } =  Guid.NewGuid();

    [Required]
    public string ProjectName { get; set; } = string.Empty;

    public string? ProjectDescription { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.UtcNow;
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

    public ICollection<ProjectMemberShip> ProjectMemberShips { get; set; } = new List<ProjectMemberShip>();

    public int MemberCount => ProjectMemberShips?.Count(m => m.DeletedAt == null) ?? 0;

    public bool IsActive => Status == ProjectStatus.Active && !IsDeleted;


    [Required]
    public Guid  WorkSpaceId { get; set; }
    [ForeignKey(nameof(WorkSpaceId))]

    public Workspace Workspace { get; set; }


    [Required]
    public Guid? CreatedByUserId { get; set; }
    [ForeignKey(nameof(CreatedByUserId))]

    public User? CreatedBy { get; set; }



    public ICollection<ProjectCategory> ProjectCategories { get; set; } = new List<ProjectCategory>();

    public ICollection<TaskItem> ProjectTasks { get; set; } = new List<TaskItem>();

    public ICollection<ProjectAttachment> ProjectAttachments { get; set; } = new List<ProjectAttachment>();



    public static void ConfigureRelations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>()
            .HasOne(ugr => ugr.CreatedBy)
            .WithMany(u => u.CreatedProjects)
            .HasForeignKey(ugr => ugr.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }

}
