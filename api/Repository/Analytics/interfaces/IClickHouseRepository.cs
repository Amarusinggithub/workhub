using api.DTOs.Analytics.ClickHouse;

namespace api.Repository.Analytics.interfaces;

public interface IClickHouseRepository
{
    // ── Writes ────────────────────────────────────────────────────────────────
    Task TrackTaskEventAsync(TaskEventRecord record);
    Task TrackUserActivityAsync(UserActivityRecord record);
    Task TrackApiRequestAsync(ApiRequestRecord record);

    // ── Task analytics ────────────────────────────────────────────────────────
    Task<IEnumerable<TaskStatusSummary>> GetTasksByStatusAsync(Guid projectId, DateTime from, DateTime to);
    Task<IEnumerable<DailyTaskCount>> GetTaskVelocityAsync(Guid projectId, DateTime from, DateTime to);
    Task<IEnumerable<ProjectVelocity>> GetProjectVelocityByWeekAsync(Guid projectId, DateTime from, DateTime to);

    // ── User analytics ────────────────────────────────────────────────────────
    Task<long> GetActiveUserCountAsync(Guid workspaceId, DateTime from, DateTime to);
    Task<IEnumerable<UserProductivity>> GetUserProductivityAsync(Guid workspaceId, DateTime from, DateTime to);

    // ── API performance ───────────────────────────────────────────────────────
    Task<IEnumerable<ApiEndpointSummary>> GetSlowEndpointsAsync(DateTime from, DateTime to, int limit = 20);
    Task<long> GetTotalRequestCountAsync(DateTime from, DateTime to);
    Task<double> GetErrorRateAsync(DateTime from, DateTime to);
}
