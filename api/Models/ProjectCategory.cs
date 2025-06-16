using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class ProjectCategory
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int ProjectId { get; set; }
    public Project Project { get; set; }

    [Required]
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}
