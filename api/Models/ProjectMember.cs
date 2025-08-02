using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class ProjectMember
{



    [Key]
    public int Id { get; set; }

    [Required]
    public Guid  UserId { get; set; }
    public User User { get; set; }

    [Required]
    public Guid  ProjectId { get; set; }
    public Project Project { get; set; }

    public DateTime AddedAt { get; set; }
    public DateTime? RemovedAt { get; set; }

}
