using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Enums;

namespace api.Models;

public class Notification
{
    [Key]
    public Guid Id { get; set; } =  Guid.NewGuid();

    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [MaxLength(1000)]
    public string? Message { get; set; }

    [MaxLength(50)]
    public string? Category { get; set; }

    public NotificationPriority Priority { get; set; } = NotificationPriority.Normal;

    [Required]
    public NotificationType Type { get; set; }
    public bool IsUrgent { get; set; } = false;

    public NotificationStatus Status { get; set; } = NotificationStatus.Pending;



    public DateTime CreatedAt { get; set; }

    public DateTime? ScheduledAt { get; set; }

    public DateTime? SentAt { get; set; }


    public DateTime? ExpiresAt { get; set; }

    [MaxLength(100)]
    public string? DeliveryChannel { get; set; }

    public bool RequiresAction { get; set; } = false;

    public int MaxRetryAttempts { get; set; } = 3;

    public int RetryCount { get; set; } = 0;

    [MaxLength(500)]
    public string? ImageUrl { get; set; }

    [MaxLength(500)]
    public string? ActionUrl { get; set; }

    [MaxLength(100)]
    public string? ActionText { get; set; }

    [Column(TypeName = "jsonb")]
    public string? Metadata { get; set; }

    [MaxLength(100)]
    public string? SourceModule { get; set; }

    [MaxLength(100)]
    public string? EventType { get; set; }


    public int ViewCount { get; set; } = 0;

    public Guid? SenderId { get; set; }

    [MaxLength(100)]
    public string? SenderName { get; set; }

    [MaxLength(50)]
    public string? SenderType { get; set; }


public UserGroup? Group { get; set; }
    public Guid? GroupId { get; set; }

    public Guid? ParentNotificationId { get; set; }

    [MaxLength(100)]
    public string? Thread { get; set; }

    [MaxLength(10)]
    public string? Language { get; set; } = "en";

    [MaxLength(10)]
    public string? TimeZone { get; set; }

    [MaxLength(50)]
    public string? DeviceType { get; set; }

    [MaxLength(50)]
    public string? Platform { get; set; }
    public ICollection<NotificationMember> UserNotifications { get; set; } = new List<NotificationMember>();
}
