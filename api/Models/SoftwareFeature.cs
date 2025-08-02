using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class SoftwareFeature
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int SoftwareId { get; set; }
    public Software Software { get; set; }

    [Required]
    [MaxLength(100)]
    public string FeatureName { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }

    [MaxLength(50)]
    public string? Icon { get; set; }

    public bool IsHighlight { get; set; } = false;

    public int SortOrder { get; set; } = 0;

    public bool IsActive { get; set; } = true;
}
