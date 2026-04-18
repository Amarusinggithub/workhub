using api.DTOs.Analytics.ClickHouse;
using api.Repository.Analytics.interfaces;
using ClickHouse.Client.ADO;
using ClickHouse.Client.Utility;

namespace api.Repository.Analytics;

public class ClickHouseRepository : IClickHouseRepository
{
    private readonly string _connectionString;
    private readonly ILogger<ClickHouseRepository> _logger;

    public ClickHouseRepository(IConfiguration config, ILogger<ClickHouseRepository> logger)
    {
        _connectionString = config.GetConnectionString("ClickHouse")
            ?? throw new InvalidOperationException("ClickHouse connection string is not configured");
        _logger = logger;
    }

    private ClickHouseConnection CreateConnection() => new(_connectionString);

    // ── Writes ────────────────────────────────────────────────────────────────

    public async Task TrackTaskEventAsync(TaskEventRecord r)
    {
        try
        {
            using var conn = CreateConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO workhub_analytics.task_events
                    (event_type, task_id, project_id, workspace_id, user_id, old_value, new_value, metadata, occurred_at)
                VALUES
                    ({event_type:String}, {task_id:UUID}, {project_id:UUID}, {workspace_id:UUID},
                     {user_id:UUID}, {old_value:Nullable(String)}, {new_value:Nullable(String)},
                     {metadata:String}, {occurred_at:DateTime64(3)})";

            cmd.AddParameter("event_type", r.EventType);
            cmd.AddParameter("task_id", r.TaskId);
            cmd.AddParameter("project_id", r.ProjectId);
            cmd.AddParameter("workspace_id", r.WorkspaceId);
            cmd.AddParameter("user_id", r.UserId);
            cmd.AddParameter("old_value", r.OldValue);
            cmd.AddParameter("new_value", r.NewValue);
            cmd.AddParameter("metadata", r.Metadata);
            cmd.AddParameter("occurred_at", r.OccurredAt ?? DateTime.UtcNow);

            await cmd.ExecuteNonQueryAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to track task event {EventType} for task {TaskId}", r.EventType, r.TaskId);
        }
    }

    public async Task TrackUserActivityAsync(UserActivityRecord r)
    {
        try
        {
            using var conn = CreateConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO workhub_analytics.user_activity
                    (user_id, workspace_id, action, resource_type, resource_id, ip_address, occurred_at)
                VALUES
                    ({user_id:UUID}, {workspace_id:UUID}, {action:String}, {resource_type:String},
                     {resource_id:UUID}, {ip_address:String}, {occurred_at:DateTime64(3)})";

            cmd.AddParameter("user_id", r.UserId);
            cmd.AddParameter("workspace_id", r.WorkspaceId);
            cmd.AddParameter("action", r.Action);
            cmd.AddParameter("resource_type", r.ResourceType);
            cmd.AddParameter("resource_id", r.ResourceId);
            cmd.AddParameter("ip_address", r.IpAddress);
            cmd.AddParameter("occurred_at", r.OccurredAt ?? DateTime.UtcNow);

            await cmd.ExecuteNonQueryAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to track user activity {Action} for user {UserId}", r.Action, r.UserId);
        }
    }

    public async Task TrackApiRequestAsync(ApiRequestRecord r)
    {
        try
        {
            using var conn = CreateConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO workhub_analytics.api_requests
                    (endpoint, method, status_code, duration_ms, user_id, workspace_id, occurred_at)
                VALUES
                    ({endpoint:String}, {method:String}, {status_code:UInt16}, {duration_ms:Float64},
                     {user_id:Nullable(UUID)}, {workspace_id:Nullable(UUID)}, {occurred_at:DateTime64(3)})";

            cmd.AddParameter("endpoint", r.Endpoint);
            cmd.AddParameter("method", r.Method);
            cmd.AddParameter("status_code", (ushort)r.StatusCode);
            cmd.AddParameter("duration_ms", r.DurationMs);
            cmd.AddParameter("user_id", r.UserId);
            cmd.AddParameter("workspace_id", r.WorkspaceId);
            cmd.AddParameter("occurred_at", r.OccurredAt ?? DateTime.UtcNow);

            await cmd.ExecuteNonQueryAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to track API request {Method} {Endpoint}", r.Method, r.Endpoint);
        }
    }

    // ── Task analytics ────────────────────────────────────────────────────────

    public async Task<IEnumerable<TaskStatusSummary>> GetTasksByStatusAsync(Guid projectId, DateTime from, DateTime to)
    {
        using var conn = CreateConnection();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            SELECT new_value AS status, count() AS count
            FROM workhub_analytics.task_events
            WHERE project_id = {project_id:UUID}
              AND event_type = 'status_changed'
              AND occurred_at BETWEEN {from:DateTime64(3)} AND {to:DateTime64(3)}
            GROUP BY new_value
            ORDER BY count DESC";

        cmd.AddParameter("project_id", projectId);
        cmd.AddParameter("from", from);
        cmd.AddParameter("to", to);

        var results = new List<TaskStatusSummary>();
        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            results.Add(new TaskStatusSummary(reader.GetString(0), reader.GetInt64(1)));

        return results;
    }

    public async Task<IEnumerable<DailyTaskCount>> GetTaskVelocityAsync(Guid projectId, DateTime from, DateTime to)
    {
        using var conn = CreateConnection();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            SELECT
                toDate(occurred_at) AS day,
                countIf(event_type = 'created')   AS created,
                countIf(event_type = 'completed') AS completed
            FROM workhub_analytics.task_events
            WHERE project_id = {project_id:UUID}
              AND occurred_at BETWEEN {from:DateTime64(3)} AND {to:DateTime64(3)}
            GROUP BY day
            ORDER BY day";

        cmd.AddParameter("project_id", projectId);
        cmd.AddParameter("from", from);
        cmd.AddParameter("to", to);

        var results = new List<DailyTaskCount>();
        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            results.Add(new DailyTaskCount(reader.GetDateTime(0), reader.GetInt64(1), reader.GetInt64(2)));

        return results;
    }

    public async Task<IEnumerable<ProjectVelocity>> GetProjectVelocityByWeekAsync(Guid projectId, DateTime from, DateTime to)
    {
        using var conn = CreateConnection();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            SELECT
                toMonday(occurred_at) AS week,
                countIf(event_type = 'created')   AS tasks_created,
                countIf(event_type = 'completed') AS tasks_completed
            FROM workhub_analytics.task_events
            WHERE project_id = {project_id:UUID}
              AND occurred_at BETWEEN {from:DateTime64(3)} AND {to:DateTime64(3)}
            GROUP BY week
            ORDER BY week";

        cmd.AddParameter("project_id", projectId);
        cmd.AddParameter("from", from);
        cmd.AddParameter("to", to);

        var results = new List<ProjectVelocity>();
        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            results.Add(new ProjectVelocity(reader.GetDateTime(0), reader.GetInt64(1), reader.GetInt64(2)));

        return results;
    }

    // ── User analytics ────────────────────────────────────────────────────────

    public async Task<long> GetActiveUserCountAsync(Guid workspaceId, DateTime from, DateTime to)
    {
        using var conn = CreateConnection();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            SELECT uniq(user_id)
            FROM workhub_analytics.user_activity
            WHERE workspace_id = {workspace_id:UUID}
              AND occurred_at BETWEEN {from:DateTime64(3)} AND {to:DateTime64(3)}";

        cmd.AddParameter("workspace_id", workspaceId);
        cmd.AddParameter("from", from);
        cmd.AddParameter("to", to);

        var result = await cmd.ExecuteScalarAsync();
        return Convert.ToInt64(result);
    }

    public async Task<IEnumerable<UserProductivity>> GetUserProductivityAsync(Guid workspaceId, DateTime from, DateTime to)
    {
        using var conn = CreateConnection();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            SELECT
                user_id,
                countIf(action = 'complete_task') AS tasks_completed,
                count()                           AS total_actions
            FROM workhub_analytics.user_activity
            WHERE workspace_id = {workspace_id:UUID}
              AND occurred_at BETWEEN {from:DateTime64(3)} AND {to:DateTime64(3)}
            GROUP BY user_id
            ORDER BY tasks_completed DESC
            LIMIT 50";

        cmd.AddParameter("workspace_id", workspaceId);
        cmd.AddParameter("from", from);
        cmd.AddParameter("to", to);

        var results = new List<UserProductivity>();
        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            results.Add(new UserProductivity(reader.GetGuid(0), reader.GetInt64(1), reader.GetInt64(2)));

        return results;
    }

    // ── API performance ───────────────────────────────────────────────────────

    public async Task<IEnumerable<ApiEndpointSummary>> GetSlowEndpointsAsync(DateTime from, DateTime to, int limit = 20)
    {
        using var conn = CreateConnection();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            SELECT
                endpoint,
                method,
                count()                                                          AS request_count,
                avg(duration_ms)                                                 AS avg_duration_ms,
                quantile(0.95)(duration_ms)                                      AS p95_duration_ms,
                quantile(0.99)(duration_ms)                                      AS p99_duration_ms,
                countIf(status_code >= 500)                                      AS error_count
            FROM workhub_analytics.api_requests
            WHERE occurred_at BETWEEN {from:DateTime64(3)} AND {to:DateTime64(3)}
            GROUP BY endpoint, method
            ORDER BY p99_duration_ms DESC
            LIMIT {limit:UInt32}";

        cmd.AddParameter("from", from);
        cmd.AddParameter("to", to);
        cmd.AddParameter("limit", (uint)limit);

        var results = new List<ApiEndpointSummary>();
        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            results.Add(new ApiEndpointSummary(
                Endpoint: reader.GetString(0),
                Method: reader.GetString(1),
                RequestCount: reader.GetInt64(2),
                AvgDurationMs: reader.GetDouble(3),
                P95DurationMs: reader.GetDouble(4),
                P99DurationMs: reader.GetDouble(5),
                ErrorCount: reader.GetInt64(6)));

        return results;
    }

    public async Task<long> GetTotalRequestCountAsync(DateTime from, DateTime to)
    {
        using var conn = CreateConnection();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            SELECT count()
            FROM workhub_analytics.api_requests
            WHERE occurred_at BETWEEN {from:DateTime64(3)} AND {to:DateTime64(3)}";

        cmd.AddParameter("from", from);
        cmd.AddParameter("to", to);

        var result = await cmd.ExecuteScalarAsync();
        return Convert.ToInt64(result);
    }

    public async Task<double> GetErrorRateAsync(DateTime from, DateTime to)
    {
        using var conn = CreateConnection();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            SELECT countIf(status_code >= 500) / count() * 100
            FROM workhub_analytics.api_requests
            WHERE occurred_at BETWEEN {from:DateTime64(3)} AND {to:DateTime64(3)}";

        cmd.AddParameter("from", from);
        cmd.AddParameter("to", to);

        var result = await cmd.ExecuteScalarAsync();
        return Convert.ToDouble(result);
    }
}
