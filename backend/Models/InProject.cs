using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class InProject
{



    [Key]
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; }
    public User User { get; set; }

    [Required]
    public int ProjectId { get; set; }
    public Project Project { get; set; }

    public DateTime AddedAt { get; set; }
    public DateTime? RemovedAt { get; set; }

}
