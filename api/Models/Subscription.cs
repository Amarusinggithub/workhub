using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Enums;

namespace api.Models;

public class Subscription
{
    [Key]
    public Guid Id { get; set; }

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
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime StartDate { get; set; } = DateTime.UtcNow;
    public DateTime? EndDate { get; set; }

    public DateTime? LastBillingDate { get; set; }
    public DateTime? NextBillingDate { get; set; }
    public BillingCycle BillingCycle { get; set; } = BillingCycle.Monthly;

    [Column(TypeName = "decimal(18,2)")]
    public decimal CurrentPrice { get; set; }

    [MaxLength(10)]
    public string Currency { get; set; } = "USD";

    public bool AutoRenew { get; set; } = true;
    public int FailedPaymentAttempts { get; set; } = 0;
    public DateTime? LastPaymentDate { get; set; }

    [MaxLength(500)]
    public string? CancellationReason { get; set; }

    [MaxLength(200)]
    public string? PaymentMethodId { get; set; }

    [MaxLength(100)]
    public string? PaymentProvider { get; set; }

    public bool IsActive => EndDate == null || EndDate > DateTime.UtcNow;
    public bool IsTrialActive => TrialPeriodStartDate.HasValue &&
                                TrialPeriodEndDate.HasValue &&
                                DateTime.UtcNow >= TrialPeriodStartDate &&
                                DateTime.UtcNow <= TrialPeriodEndDate;

    public ICollection<PlanHistory> PlanHistories { get; set; } = new List<PlanHistory>();
    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
