using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class CommentReaction
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid CommentId { get; set; }
    public Comment Comment { get; set; } = null!;

    [Required]
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    [Required]
    [StringLength(20)]
    public string ReactionType { get; set; } = string.Empty; // Like, Dislike, Love, etc.

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
