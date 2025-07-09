using System.ComponentModel.DataAnnotations;
using api.Enums;

namespace api.Models;
public class UserResource
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int TaskResourceId { get; set; }
    public TaskAttachment TaskAttachment { get; set; }

    [Required]
    public int UserId { get; set; }
    public User User { get; set; }

    public ResorceLocation ResourceLocation { get; set; }

    public bool IsTrashed { get; set; } = false;
    public bool IsFavorite { get; set; } = false;
    public bool IsPinned { get; set; } = false;

    [Required]
    public int RoleId { get; set; }
    public Role Role { get; set; }

}
