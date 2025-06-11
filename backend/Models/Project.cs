using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class Project
{

    [Key]
    public int Id { get; set; }

    [Required]
    public string ProjectName { get; set; } = string.Empty;

    public string? ProjectDescription { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public DateTime? ScheduledFinishAt { get; set; }
    public DateTime? FinishedAt { get; set; }
    public DateTime? ArchivedAt { get; set; }
    public DateTime? ScheduledDeleteAt { get; set; }

    public bool IsArchived { get; set; } = false;
    public bool IsTrashed { get; set; } = false;
    public DateTime? TrashedAt { get; set; }


    [Required]
    public int WorkSpaceId { get; set; }
    public WorkSpace WorkSpace { get; set; }

    public ICollection<UserProjectRole> UserProjectRoles { get; set; } = new List<UserProjectRole>();


    public ICollection<ProjectMember> UserProjects { get; set; } = new List<ProjectMember>();
    public ICollection<ProjectCategory> ProjectCategories { get; set; } = new List<ProjectCategory>();

}
