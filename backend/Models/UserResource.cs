using System.ComponentModel.DataAnnotations;
using backend.Enums;

namespace backend.Models;
public class UserResource
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int ResourceId { get; set; }
    public Resource Resource { get; set; }

    [Required]
    public int UserId { get; set; }
    public User User { get; set; }

    public ResorceLocation ResourceLocation { get; set; }
    public DateTime LastDownloadAt { get; set; }
    public DateTime LastOpenAt { get; set; }
    public bool IsTrashed { get; set; } = false;
    public bool IsFavorite { get; set; } = false;
    public bool IsPinned { get; set; } = false;

    [Required]
    public int RoleId { get; set; }
    public Role Role { get; set; }

}
