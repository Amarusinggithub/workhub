using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Enums;

namespace api.Models;
public class UserResource
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int ResourceId { get; set; }
    [ForeignKey(nameof(ResourceId))]

    public Resource Resource { get; set; }

    [Required]
    public Guid  UserId { get; set; }
    [ForeignKey(nameof(UserId))]

    public User User { get; set; }

    public UserResourceLocation ResourceLocation { get; set; }

    public bool IsPinned { get; set; } = false;


    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.UtcNow;


}
