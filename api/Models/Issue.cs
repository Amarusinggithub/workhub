using System.ComponentModel.DataAnnotations;
using api.Enums;

namespace api.Models;

public class Issue
{



    [Key]
    public int Id { get; set; }

    [Required]
    public string TaskName { get; set; } = string.Empty;
    public string? TaskDescription { get; set; }

    public IssueStatus IssueStatus { get; set; }
    public IssueType IssueType { get; set; }
    public IssuePriority IssuePriority { get; set; }

    public int? AssignedToId { get; set; }
    public User? AssignedTo { get; set; }

    [Required]
    public int ProjectId { get; set; }
    public Project Project { get; set; }


    public int ParentId { get; set; }
    public ICollection<Issue> Issues { get; set; } = new List<Issue>();

    public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public ICollection<IssueLabel> Labels { get; set; } = new List<IssueLabel>();

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? FinishedAt { get; set; }

}
