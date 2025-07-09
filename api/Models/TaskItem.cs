using System.ComponentModel.DataAnnotations;
using api.Enums;
using TaskStatus = api.Enums.TaskStatus;

namespace api.Models;

public class TaskItem
{

    [Key]
    public int Id { get; set; }

    [Required]
    public string TaskName { get; set; } = string.Empty;
    public string? TaskDescription { get; set; }

    public TaskStatus TaskStatus { get; set; }
    public TaskType TaskType { get; set; }
    public TaskPriority TaskPriority { get; set; }

    [Required]
    public int WorkspaceId { get; set; }
    public Workspace Workspace { get; set; }

    [Required]
    public int  ProjectId { get; set; }
    public Project Project { get; set; }


    public int ParentId { get; set; }
    public ICollection<TaskItem>? Issues { get; set; } = new List<TaskItem>();

    public ICollection<Comment>? Comments { get; set; } = new List<Comment>();

    public ICollection<TaskLabel>? Labels { get; set; } = new List<TaskLabel>();

    public ICollection<TaskAttachment>? Attactments { get; set; } = new List<TaskAttachment>();
    public ICollection<TaskAssignment>? Assignments { get; set; } = new List<TaskAssignment>();


    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? FinishedAt { get; set; }

}
