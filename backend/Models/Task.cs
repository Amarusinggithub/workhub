using System.ComponentModel.DataAnnotations;
using TaskStatus = backend.Enums.TaskStatus;

namespace backend.Models;

public class Task
{



    [Key]
    public int Id { get; set; }

    [Required]
    public string TaskName { get; set; } = string.Empty;
    public string? TaskDescription { get; set; }

    public TaskStatus TaskStatus { get; set; }

    public int? AssignedToId { get; set; }
    public User? AssignedTo { get; set; }

    [Required]
    public int ProjectId { get; set; }
    public Project Project { get; set; }




    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public DateTime? FinishedAt { get; set; }

}
