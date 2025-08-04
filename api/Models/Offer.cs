using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Enums;
using Microsoft.EntityFrameworkCore;

namespace api.Models;

public class Offer
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string OfferName { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? OfferDescription { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal DiscountAmount { get; set; }

    [Column(TypeName = "decimal(5,2)")]
    public decimal DiscountPercentage { get; set; }

    public int DurationMonths { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public bool IsActive { get; set; } = true;
    public bool IsPublic { get; set; } = true;
    public int MaxRedemptions { get; set; } = 0;
    public int CurrentRedemptions { get; set; } = 0;

    [MaxLength(50)]
    public string? CouponCode { get; set; }

    public bool RequiresCouponCode { get; set; } = false;
    public bool IsFirstTimeCustomerOnly { get; set; } = false;
    public bool IsRecurring { get; set; } = false;

    public OfferType Type { get; set; } = OfferType.Percentage;

    [Column(TypeName = "decimal(18,2)")]
    public decimal? MinimumOrderAmount { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public bool IsValid => IsActive &&
                           (!StartDate.HasValue || StartDate.Value <= DateTime.UtcNow) &&
                           (!EndDate.HasValue || EndDate.Value >= DateTime.UtcNow) &&
                           (MaxRedemptions == 0 || CurrentRedemptions < MaxRedemptions);

    public ICollection<Include> Includes { get; set; } = new List<Include>();
    public ICollection<Prerequisite> Prerequisites { get; set; } = new List<Prerequisite>();
    public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();


    public static void ConfigureRelations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Offer>()
            .HasMany(o => o.Subscriptions)
            .WithOne(s => s.Offer)
            .HasForeignKey(s => s.OfferId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
