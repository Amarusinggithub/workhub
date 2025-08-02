using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class UserPermission
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    [Required]
    [StringLength(100)]
    public string Permission { get; set; } = string.Empty;

    [StringLength(100)]
    public string? Resource { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ExpiresAt { get; set; }
}
