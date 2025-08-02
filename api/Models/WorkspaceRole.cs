using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class WorkspaceRole
{
    [Key]
    public int Id { get; set; }

    [Required]
    public Guid  UserId { get; set; }
    public User User { get; set; }

    [Required]
    public Guid  WorkspaceId { get; set; }
    public Workspace Workspace { get; set; }

    [Required]
    public Guid RoleId { get; set; }
    public Role Role { get; set; }

    public DateTime AssignedAt { get; set; }
    public DateTime? RemovedAt { get; set; }
}
