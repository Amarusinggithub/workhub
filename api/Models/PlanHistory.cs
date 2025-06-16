using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class PlanHistory
{


    [Key]
    public int Id { get; set; }

    [Required]
    public int SubscriptionId { get; set; }
    public Subscription Subscription { get; set; }

    [Required]
    public int PlanId { get; set; }
    public Plan Plan { get; set; }

    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

}
