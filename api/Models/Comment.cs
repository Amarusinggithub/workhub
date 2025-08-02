using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class Comment
{
    [Key]
    public Guid  Id { get; set; }
    [Required]
    public Guid  CommenterId { get; set; }
    public User Commenter { get; set; }

    public string Message { get; set; }

    public ICollection<Comment> Replies { get; set; } = new List<Comment>();

    public Guid? ParentId { get; set; }
    public Comment?Parent { get; set; }


    [Required]
    public Guid  TaskId { get; set; }
    public TaskItem TaskItem { get; set; }

    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}
