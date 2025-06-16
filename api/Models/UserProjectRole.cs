using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class UserProjectRole
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }
    public User User { get; set; }

    [Required]
    public int ProjectId { get; set; }
    public Project Project { get; set; }

    [Required]
    public int RoleId { get; set; }
    public Role Role { get; set; }

    public DateTime AssignedAt { get; set; }
    public DateTime? RemovedAt { get; set; }
}
