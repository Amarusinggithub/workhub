using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class TaskAssignment
{
    [Key]
    public int Id { get; set; }
    public int TaskItemId { get; set; }
    public TaskItem TaskItem { get; set; }

    public int? AssignedToId { get; set; }
    public User? AssignedTo { get; set; }

    public DateTime CreatedAt { get; set; }

}

