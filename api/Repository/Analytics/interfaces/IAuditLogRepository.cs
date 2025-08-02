using api.Models;
using api.Repository.interfaces;

namespace api.Repository.Analytics.interfaces;

public interface IAuditLogRepository:IGenericRepository<AuditLog,int>
{

}
