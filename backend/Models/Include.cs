using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class Include
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int OfferId { get; set; }
    public Offer Offer { get; set; }

    [Required]
    public int PlanId { get; set; }
    public Plan Plan { get; set; }
}
