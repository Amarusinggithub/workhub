namespace api.DTOs.Analytics.ClickHouse;

public record TaskStatusSummary(string Status, long Count);

public record DailyTaskCount(DateTime Date, long Created, long Completed);

public record UserProductivity(Guid UserId, long TasksCompleted, long TotalActions);

public record ProjectVelocity(DateTime Week, long TasksCreated, long TasksCompleted);

public record ApiEndpointSummary(
    string Endpoint,
    string Method,
    long RequestCount,
    double AvgDurationMs,
    double P95DurationMs,
    double P99DurationMs,
    long ErrorCount);
