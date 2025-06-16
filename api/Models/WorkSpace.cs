using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class WorkSpace
{

    [Key]
    public int Id { get; set; }

    [Required]
    public string WorkSpaceName { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public ICollection<Project> Projects { get; set; } = new List<Project>();
    public ICollection<WorkSpaceMember> UserWorkSpaces { get; set; } = new List<WorkSpaceMember>();
}
