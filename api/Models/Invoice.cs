using System.ComponentModel.DataAnnotations;
using api.Enums;

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
    public Subscription Subscription { get; set; }

    [Required]
    public int PlanHistoryId { get; set; }
    public PlanHistory PlanHistory { get; set; }
}
