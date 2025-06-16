using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class OptionIncluded
{


    [Key]
    public int Id { get; set; }

    [Required]
    public int PlanId { get; set; }
    public Plan Plan { get; set; }

    [Required]
    public int OptionId { get; set; }
    public Option Option { get; set; }

    public DateTime AddedAt { get; set; }
    public DateTime? RemovedAt { get; set; }


}
