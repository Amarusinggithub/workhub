using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class CommentHistory
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid CommentId { get; set; }
    public Comment Comment { get; set; } = null!;

    [Required]
    [StringLength(50)]
    public string Action { get; set; } = string.Empty; // Created, Edited, Deleted, etc.

    [StringLength(10000)]
    public string? PreviousMessage { get; set; }

    [StringLength(10000)]
    public string? NewMessage { get; set; }

    [Required]
    public Guid PerformedById { get; set; }
    public User PerformedBy { get; set; } = null!;

    public DateTime PerformedAt { get; set; } = DateTime.UtcNow;
}
