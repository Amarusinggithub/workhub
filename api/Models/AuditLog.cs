using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class AuditLog
{
    [Key]
    public Guid Id { get; set; }
    public string Action { get; set; }
    public string  UserId { get; set; }
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

    public string? EntityId { get; set; }
    public string? EntityName { get; set; }
}
