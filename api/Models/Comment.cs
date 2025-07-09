using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class Comment
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int CommenterId { get; set; }
    public User Commenter { get; set; }

    public string Message { get; set; }

    public ICollection<Comment> Replies { get; set; } = new List<Comment>();

    public int? ParentId { get; set; }
    public Comment?Parent { get; set; }


    [Required]
    public int TaskId { get; set; }
    public TaskItem TaskItem { get; set; }

    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}
