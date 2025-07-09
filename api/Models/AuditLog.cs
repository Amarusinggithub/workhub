using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class AuditLog
{
    [Key]
    public Guid Id { get; set; }
    public string Action { get; set; }
    public int  UserId { get; set; }
    public User User { get; set; }

    public DateTime PerformedAt { get; set; }
    public string AffectedEntity { get; set; }
    public string? MetadataJson { get; set; }

    public string? BrowserInfo { get; set; }
    public string? HttpMethod { get; set; }
    public string? Url { get; set; }

    public string? ServiceName { get; set; }
    public string? MethodName { get; set; }
    public string? Parameters { get; set; }

    public int? EntityId { get; set; }
    public string? EntityName { get; set; }
    public string EntityType { get; set; }

    public Guid PerformedById { get; set; }
    public User PerformedBy { get; set; } = null!;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
