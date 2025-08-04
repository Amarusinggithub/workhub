using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class Label
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Discription { get; set; }

    public ICollection<TaskLabel> Issues { get; set; } = new List<TaskLabel>();


    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.UtcNow;



}
