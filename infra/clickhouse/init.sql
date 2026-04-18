CREATE DATABASE IF NOT EXISTS workhub_analytics;

-- Every state change on a task lands here.
-- Partition by month so old data ages out cheaply.
-- ORDER BY puts the most common query pattern first (workspace → project → time).
CREATE TABLE IF NOT EXISTS workhub_analytics.task_events
(
    event_id     UUID DEFAULT generateUUIDv4(),
    event_type   LowCardinality(String),   -- 'created' | 'updated' | 'status_changed' | 'assigned' | 'completed' | 'deleted'
    task_id      UUID,
    project_id   UUID,
    workspace_id UUID,
    user_id      UUID,
    old_value    Nullable(String),
    new_value    Nullable(String),
    metadata     String DEFAULT '{}',
    occurred_at  DateTime64(3, 'UTC')
)
ENGINE = MergeTree()
PARTITION BY toYYYYMM(occurred_at)
ORDER BY (workspace_id, project_id, occurred_at)
TTL occurred_at + INTERVAL 2 YEAR;

-- Every meaningful user action (page view, create, delete, assign, etc.)
CREATE TABLE IF NOT EXISTS workhub_analytics.user_activity
(
    activity_id   UUID DEFAULT generateUUIDv4(),
    user_id       UUID,
    workspace_id  UUID,
    action        LowCardinality(String),         -- 'login' | 'create_task' | 'assign_task' etc.
    resource_type LowCardinality(String),         -- 'task' | 'project' | 'workspace'
    resource_id   UUID,
    ip_address    String DEFAULT '',
    occurred_at   DateTime64(3, 'UTC')
)
ENGINE = MergeTree()
PARTITION BY toYYYYMM(occurred_at)
ORDER BY (workspace_id, user_id, occurred_at)
TTL occurred_at + INTERVAL 1 YEAR;

-- Every HTTP request your API receives — latency, status, endpoint.
-- Short TTL: 90 days is plenty for performance analysis.
CREATE TABLE IF NOT EXISTS workhub_analytics.api_requests
(
    request_id   UUID DEFAULT generateUUIDv4(),
    endpoint     String,
    method       LowCardinality(String),
    status_code  UInt16,
    duration_ms  Float64,
    user_id      Nullable(UUID),
    workspace_id Nullable(UUID),
    occurred_at  DateTime64(3, 'UTC')
)
ENGINE = MergeTree()
PARTITION BY toYYYYMM(occurred_at)
ORDER BY (occurred_at, endpoint)
TTL occurred_at + INTERVAL 90 DAY;
