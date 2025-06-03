using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class Software
{
[Key]
public int Id { get; set; }

[Required]
public string SoftwareName { get; set; } = string.Empty;
public string? Details { get; set; }
public string? AccessLink { get; set; }

public ICollection<Plan> Plans { get; set; } = new List<Plan>();
}
