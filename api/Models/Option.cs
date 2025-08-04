using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class Option
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string OptionName { get; set; } = string.Empty;
    public ICollection<OptionIncluded> OptionIncludes { get; set; } = new List<OptionIncluded>();

    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.UtcNow;
}
