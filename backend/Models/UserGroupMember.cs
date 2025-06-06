using System.ComponentModel.DataAnnotations;
using backend.Enums;

namespace backend.Models;

public class UserGroupMember
{

    [Key]
    public int Id { get; set; }

    [Required]
    public int UserGroupId { get; set; }
    public UserGroup UserGroup { get; set; }

    [Required]
    public int UserId { get; set; }
    public User User { get; set; }

    public DateTime AddedAt { get; set; }
    public DateTime? RemovedAt { get; set; }

    public bool IsAdmin { get; set; } = false;


}
