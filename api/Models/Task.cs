using System.ComponentModel.DataAnnotations;
using api.Enums;
using TaskStatus = api.Enums.TaskStatus;

namespace api.Models;

public class Task
{

    [Key]
    public int Id { get; set; }

    [Required]
    public string TaskName { get; set; } = string.Empty;
    public string? TaskDescription { get; set; }

    public TaskStatus TaskStatus { get; set; }
    public TaskType TaskType { get; set; }
    public TaskPriority TaskPriority { get; set; }

    public int? AssignedToId { get; set; }
    public User? AssignedTo { get; set; }

    [Required]
    public int ProjectId { get; set; }
    public Project Project { get; set; }


    public int ParentId { get; set; }
    public ICollection<Task> Issues { get; set; } = new List<Task>();

    public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public ICollection<TaskLabel> Labels { get; set; } = new List<TaskLabel>();

    public ICollection<TaskResource> resources { get; set; } = new List<TaskResource>();


    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? FinishedAt { get; set; }

}
