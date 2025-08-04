using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace api.Models;

public class AuditLog
{
    [Key] public int Id { get; set; }

    [Required] [StringLength(100)] public string Action { get; set; } = string.Empty;

    [StringLength(500)] public string? BrowserInfo { get; set; }

    [StringLength(10)] public string? HttpMethod { get; set; }

    [StringLength(2000)] public string? Url { get; set; }

    [StringLength(45)] // IPv6 max length
    public string? IpAddress { get; set; }

    [StringLength(100)] public string? ServiceName { get; set; }

    [StringLength(100)] public string? MethodName { get; set; }

    [Column(TypeName = "text")] public string? Parameters { get; set; }

    [Column(TypeName = "jsonb")] public string? MetadataJson { get; set; }

    [StringLength(50)] public string? EntityId { get; set; }

    [StringLength(100)] public string? EntityName { get; set; }

    [Required] [StringLength(100)] public string EntityType { get; set; } = string.Empty;

    public Guid? PerformedById { get; set; }

    [ForeignKey(nameof(PerformedById))] public User? PerformedBy { get; set; }

    [StringLength(36)] public string? CorrelationId { get; set; }

    public DateTime PerformedAt { get; set; } = DateTime.UtcNow;

    [StringLength(50)] public string? Severity { get; set; } = "Info";

    [StringLength(20)] public string? Source { get; set; } = "Application";

    public bool IsSuccess { get; set; } = true;

    [StringLength(1000)] public string? ErrorMessage { get; set; }

    [StringLength(4000)] public string? StackTrace { get; set; }

    public long? ExecutionTimeMs { get; set; }

    [StringLength(100)] public string? UserAgent { get; set; }

    [StringLength(50)] public string? SessionId { get; set; }

    [StringLength(100)] public string? RequestId { get; set; }

    // Helper methods for metadata handling
    public T? GetMetadata<T>() where T : class
    {
        if (string.IsNullOrEmpty(MetadataJson)) return null;

        try
        {
            return JsonSerializer.Deserialize<T>(MetadataJson);
        }
        catch
        {
            return null;
        }
    }

    public void SetMetadata<T>(T metadata) where T : class
    {
        if (metadata == null)
        {
            MetadataJson = null;
            return;
        }

        try
        {
            MetadataJson = JsonSerializer.Serialize(metadata);
        }
        catch
        {
            MetadataJson = null;
        }
    }


    // Indexing hints for Entity Framework
    public static void ConfigureIndexes(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuditLog>()
            .HasIndex(a => a.PerformedAt)
            .HasDatabaseName("IX_AuditLog_PerformedAt");

        modelBuilder.Entity<AuditLog>()
            .HasIndex(a => a.PerformedById)
            .HasDatabaseName("IX_AuditLog_PerformedById");

        modelBuilder.Entity<AuditLog>()
            .HasIndex(a => new { a.EntityType, a.EntityId })
            .HasDatabaseName("IX_AuditLog_Entity");

        modelBuilder.Entity<AuditLog>()
            .HasIndex(a => a.CorrelationId)
            .HasDatabaseName("IX_AuditLog_CorrelationId");

        modelBuilder.Entity<AuditLog>()
            .HasIndex(a => a.Action)
            .HasDatabaseName("IX_AuditLog_Action");
    }
}
