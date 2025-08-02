using System.ComponentModel.DataAnnotations;
using api.Enums;

namespace api.Models;

public class Prerequisite
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int OfferId { get; set; }
    public Offer Offer { get; set; }

    [Required]
    public int PlanId { get; set; }
    public Plan Plan { get; set; }

    public PrerequisiteType Type { get; set; } = PrerequisiteType.Required;
    public int MinimumMonths { get; set; } = 0;
    public bool IsActive { get; set; } = true;
}

