using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class Offer
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string OfferName { get; set; } = string.Empty;
    public string? OfferDescription { get; set; }

    public decimal DiscountAmount { get; set; }
    public decimal DiscountPercentage { get; set; }

    public int DurationMonths { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public ICollection<Include> Includes { get; set; } = new List<Include>();
    public ICollection<Prerequisite> Prerequisites { get; set; } = new List<Prerequisite>();
    public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

}
