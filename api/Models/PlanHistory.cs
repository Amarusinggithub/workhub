using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Enums;
using Microsoft.EntityFrameworkCore;

namespace api.Models;

public class PlanHistory
{
    [Key]
    public int Id { get; set; }

    [Required]
    public Guid SubscriptionId { get; set; }

    [ForeignKey(nameof(SubscriptionId))]

    public Subscription Subscription { get; set; }

    [Required]
    public int PlanId { get; set; }

    [ForeignKey(nameof(PlanId))]

    public Plan Plan { get; set; }

    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.UtcNow;
    public DateTime? EffectiveDate { get; set; }
    public DateTime? EndDate { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal PriceAtTime { get; set; }

    public PlanChangeReason ChangeReason { get; set; }

    [MaxLength(500)]
    public string? Notes { get; set; }

    [ForeignKey(nameof(ChangedByUserId))]
    public User ? ChangedBy { get; set; }
    public Guid? ChangedByUserId { get; set; }
    public bool IsProration { get; set; } = false;

    [Column(TypeName = "decimal(18,2)")]
    public decimal? ProrationAmount { get; set; }

    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public static void ConfigureRelations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PlanHistory>()
            .HasOne(ph => ph.Subscription)
            .WithMany(s => s.PlanHistories)
            .HasForeignKey(ph => ph.SubscriptionId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<PlanHistory>()
            .HasOne(ph => ph.Plan)
            .WithMany(p => p.PlanHistories)
            .HasForeignKey(ph => ph.PlanId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
