using System.ComponentModel.DataAnnotations;
using api.Enums;

namespace api.Models;
public class UserResource
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int TaskAttachmentId { get; set; }
    public TaskAttachment TaskAttachment { get; set; }

    [Required]
    public Guid  UserId { get; set; }
    public User User { get; set; }

    public UserResourceLocation ResourceLocation { get; set; }

    public bool IsTrashed { get; set; } = false;
    public bool IsFavorite { get; set; } = false;
    public bool IsPinned { get; set; } = false;

    [Required]
    public int RoleId { get; set; }
    public Role Role { get; set; }

}
