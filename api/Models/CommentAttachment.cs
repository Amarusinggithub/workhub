using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class CommentAttachment
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid CommentId { get; set; }
    public Comment Comment { get; set; } = null!;

    [Required]
    public Guid AttactmentId { get; set; }
    public Resource Attactment { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
