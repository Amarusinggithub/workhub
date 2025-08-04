using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Enums;
using Microsoft.EntityFrameworkCore;
using TaskStatus = api.Enums.TaskStatus;

namespace api.Models;

public class TaskItem
{

    [Key]
    public Guid Id { get; set; } =  Guid.NewGuid();

    [Required]
    public string TaskName { get; set; } = string.Empty;
    public string? TaskDescription { get; set; }

    public TaskStatus TaskStatus { get; set; }
    public TaskType TaskType { get; set; }
    public TaskPriority TaskPriority { get; set; }

    [Required]
    public Guid  WorkspaceId { get; set; }
    [ForeignKey(nameof(WorkspaceId))]

    public Workspace Workspace { get; set; }

    [Required]
    public Guid   ProjectId { get; set; }
    [ForeignKey(nameof(ProjectId))]

    public Project Project { get; set; }


    public Guid ? ParentId { get; set; }
    [ForeignKey(nameof(ParentId))]
    public TaskItem? Parent { get; set; }

    public ICollection<TaskItem>? Issues { get; set; } = new List<TaskItem>();

    public ICollection<Comment>? Comments { get; set; } = new List<Comment>();

    public ICollection<TaskLabel>? Labels { get; set; } = new List<TaskLabel>();

    public ICollection<TaskAttachment>? Attactments { get; set; } = new List<TaskAttachment>();
    public ICollection<TaskAssignment>? Assignments { get; set; } = new List<TaskAssignment>();

    [Required]
    public Guid? CreatedByUserId { get; set; }
    [ForeignKey(nameof(CreatedByUserId))]

    public User? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.UtcNow;
    public DateTime? FinishedAt { get; set; }

    public static void ConfigureRelations(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<TaskItem>()
            .HasOne(pre => pre.CreatedBy)
            .WithMany(o => o.CreatedTasks)
            .HasForeignKey(pre => pre.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}



