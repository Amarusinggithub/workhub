using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class CommentMention
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid CommentId { get; set; }
    public Comment Comment { get; set; } = null!;

    [Required]
    public Guid MentionedUserId { get; set; }
    public User MentionedUser { get; set; } = null!;

    public bool IsNotified { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
