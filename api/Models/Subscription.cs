using System.ComponentModel.DataAnnotations;
using api.Enums;

namespace api.Models;

public class Subscription
{

    [Key]
    public int Id { get; set; }

    [Required]
    public int UserGroupId { get; set; }
    public UserGroup UserGroup { get; set; }

    [Required]
    public int CurrentPlanId { get; set; }
    public Plan CurrentPlan { get; set; }

    public int? OfferId { get; set; }
    public Offer? Offer { get; set; }

    public bool SubscribeAfterTrial { get; set; } = false;
    public SubscriptionStatus Status { get; set; }

    public DateTime? TrialPeriodStartDate { get; set; }
    public DateTime? TrialPeriodEndDate { get; set; }
    public DateTime? ValidTo { get; set; }

    public DateTime? UnSubscribedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime StartDate { get; set; } = DateTime.UtcNow;
    public DateTime? EndDate { get; set; }
    public bool IsActive => EndDate == null || EndDate > DateTime.UtcNow;


    public ICollection<PlanHistory> PlanHistories { get; set; } = new List<PlanHistory>();
    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}

