using api.Data;
using api.Models;
using api.Repository.Analytics.interfaces;

namespace api.Repository.Analytics;

public class AuditLogRepository:GenericRepository<AuditLog>,IAuditLogRepository
{
    public AuditLogRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
    {
    }
}
