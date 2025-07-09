namespace api.Models;

public class TaskAssignment
{
    public int TaskItemId { get; set; }
    public TaskItem TaskItem { get; set; }

    public int? AssignedToId { get; set; }
    public User? AssignedTo { get; set; }

    public DateTime CreatedAt { get; set; }

}

