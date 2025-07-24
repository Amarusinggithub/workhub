using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

public class AuditLog
{
    [Key]
    public int Id { get; set; }

    public string Action { get; set; } = null!;
    public string? BrowserInfo { get; set; }
    public string? HttpMethod { get; set; }
    public string? Url { get; set; }
    public string? IpAddress { get; set; }

    public string? ServiceName { get; set; }
    public string? MethodName { get; set; }
    public string? Parameters { get; set; }

    [Column(TypeName = "jsonb")]
    public string? MetadataJson { get; set; }

    public int? EntityId { get; set; }
    public string? EntityName { get; set; }
    public string EntityType { get; set; } = null!;

    public int? PerformedById { get; set; }
    public User? PerformedBy { get; set; }

    public string? CorrelationId { get; set; }

    public DateTime PerformedAt { get; set; } = DateTime.UtcNow;
}
