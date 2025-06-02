using System.ComponentModel.DataAnnotations;
using backend.Enums;

namespace backend.Models;

public class Subscription
{
    [Key]
    public int Id { get; set; }
    public User User { get; set; }
    public int UserId { get; set; }
    public Plan Plan { get; set; }
    public int PlanId { get; set; }
    public SubscriptionStatus Status { get; set; }
    public PaymentMethod  PaymentMethod { get; set; }
    public DateTime StaterdAt { get; set; }
    public DateTime EndedAt { get; set; }
    public DateTime PurchasedAt { get; set; }
    public DateTime TrialStaterdAt { get; set; }
    public DateTime TrialEndedAt { get; set; }



}
