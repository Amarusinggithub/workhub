using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class TaskAssignment
{
    [Key]
    public int Id { get; set; }
    public Guid TaskItemId { get; set; }
    public TaskItem TaskItem { get; set; }

    public Guid ? AssignedToUserId { get; set; }
    public User? AssignedToUser { get; set; }

    public Guid  AssignedByUserId { get; set; }
    public User AssignedByUser { get; set; }

    public DateTime CreatedAt { get; set; }

}

