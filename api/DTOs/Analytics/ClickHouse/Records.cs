namespace api.DTOs.Analytics.ClickHouse;

public record TaskEventRecord(
    string EventType,
    Guid TaskId,
    Guid ProjectId,
    Guid WorkspaceId,
    Guid UserId,
    string? OldValue = null,
    string? NewValue = null,
    string Metadata = "{}",
    DateTime? OccurredAt = null);

public record UserActivityRecord(
    Guid UserId,
    Guid WorkspaceId,
    string Action,
    string ResourceType,
    Guid ResourceId,
    string IpAddress = "",
    DateTime? OccurredAt = null);

public record ApiRequestRecord(
    string Endpoint,
    string Method,
    int StatusCode,
    double DurationMs,
    Guid? UserId = null,
    Guid? WorkspaceId = null,
    DateTime? OccurredAt = null);
