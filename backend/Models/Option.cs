using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class Option
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string OptionName { get; set; } = string.Empty;
    public ICollection<OptionIncluded> OptionIncludes { get; set; } = new List<OptionIncluded>();


}
