using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string CategoryName { get; set; } = string.Empty;
    public string? CategoryDescription { get; set; }

    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.UtcNow;

    public ICollection<ProjectCategory> ProjectCategories { get; set; } = new List<ProjectCategory>();

}
