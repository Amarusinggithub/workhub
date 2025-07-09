using System.ComponentModel.DataAnnotations;
using api.Enums;
namespace api.Models;

public class WorkSpaceMember
{

    [Key]
    public int Id { get; set; }

    [Required]
    public int WorkSpaceId { get; set; }
    public Workspace Workspace { get; set; }

    [Required]
    public int UserId { get; set; }
    public User User { get; set; }

    public DateTime AddedAt { get; set; }
    public DateTime? RemovedAt { get; set; }

    public Role Role { get; set; }
    public int RoleId { get; set; }


}
