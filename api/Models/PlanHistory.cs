using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Enums;

namespace api.Models;

public class PlanHistory
{
    [Key]
    public int Id { get; set; }

    [Required]
    public Guid SubscriptionId { get; set; }
    public Subscription Subscription { get; set; }

    [Required]
    public int PlanId { get; set; }
    public Plan Plan { get; set; }

    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
    public DateTime? EffectiveDate { get; set; }
    public DateTime? EndDate { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal PriceAtTime { get; set; }

    public PlanChangeReason ChangeReason { get; set; }

    [MaxLength(500)]
    public string? Notes { get; set; }

    public Guid? ChangedByUserId { get; set; }
    public bool IsProration { get; set; } = false;

    [Column(TypeName = "decimal(18,2)")]
    public decimal? ProrationAmount { get; set; }

    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
