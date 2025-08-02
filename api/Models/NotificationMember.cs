using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Enums;

namespace api.Models;



    public class NotificationMember
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public User User { get; set; }
        public Guid UserId { get; set; }

        [Required]
        public Notification Notification { get; set; }
        public Guid NotificationId { get; set; }

        public bool IsRead { get; set; } = false;
        public DateTime? ReadAt { get; set; }

        public bool IsClicked { get; set; } = false;
        public DateTime? ClickedAt { get; set; }

        public bool IsArchived { get; set; } = false;
        public DateTime? ArchivedAt { get; set; }

        public bool IsDismissed { get; set; } = false;
        public DateTime? DismissedAt { get; set; }

        public NotificationDeliveryStatus DeliveryStatus { get; set; } = NotificationDeliveryStatus.Pending;

        public DateTime? DeliveredAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int ViewCount { get; set; } = 0;
        public DateTime? LastViewedAt { get; set; }

        [MaxLength(50)]
        public string? DeliveryChannel { get; set; }

        [MaxLength(100)]
        public string? DeviceId { get; set; }

        [MaxLength(50)]
        public string? Platform { get; set; }

        public int RetryCount { get; set; } = 0;
        public DateTime? LastRetryAt { get; set; }

        [MaxLength(500)]
        public string? DeliveryError { get; set; }

        [Column(TypeName = "jsonb")]
        public string? UserPreferences { get; set; }

        public bool RequiresConfirmation { get; set; } = false;
        public bool IsConfirmed { get; set; } = false;
        public DateTime? ConfirmedAt { get; set; }

        [MaxLength(200)]
        public string? UserResponse { get; set; }

        public NotificationPriority? UserPriority { get; set; }

        [MaxLength(10)]
        public string? Language { get; set; }

        [MaxLength(10)]
        public string? TimeZone { get; set; }
    }




