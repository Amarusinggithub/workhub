using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Enums;
using Microsoft.EntityFrameworkCore;

namespace api.Models;
using api.Enums;
public class Invoice
{



    [Key]
    public Guid Id { get; set; } =  Guid.NewGuid();

    [Required]
    public string CustomerDataInvoiceText { get; set; } = string.Empty;

    public PaymentMethod PaymentMethod { get; set; }
    public PaymentStatuses PaymentStatus { get; set; }

    public decimal InvoiceAmount { get; set; }
    public string? InvoiceDescription { get; set; }

    public DateTime InvoicePeriodStartAt { get; set; }
    public DateTime InvoicePeriodEndAt { get; set; }
    public DateTime InvoiceCreatedAt { get; set; }
    public DateTime InvoiceDueAt { get; set; }
    public DateTime? InvoicePaidAt { get; set; }

    [Required]
    public Guid  SubscriptionId { get; set; }

    [ForeignKey(nameof(SubscriptionId))]

    public Subscription Subscription { get; set; }

    [Required]
    public int PlanHistoryId { get; set; }
    [ForeignKey(nameof(PlanHistoryId))]

    public PlanHistory PlanHistory { get; set; }

    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.UtcNow;


    public static void ConfigureRelations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Invoice>()
            .HasOne(inv => inv.Subscription)
            .WithMany(s => s.Invoices)
            .HasForeignKey(inv => inv.SubscriptionId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Invoice>()
            .HasOne(inv => inv.PlanHistory)
            .WithMany(ph => ph.Invoices)
            .HasForeignKey(inv => inv.PlanHistoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
