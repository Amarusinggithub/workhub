using System.ComponentModel.DataAnnotations;

namespace backend.Models;
using backend.Enums;
public class Invoice
{



    [Key]
    public int Id { get; set; }

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
    public int SubscriptionId { get; set; }
    public Subscription Subscription { get; set; }

    [Required]
    public int PlanHistoryId { get; set; }
    public PlanHistory PlanHistory { get; set; }
}
