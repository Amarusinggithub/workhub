using api.Data;
using api.Repository.Analytics.interfaces;

namespace api.Repository.Analytics;

public class ActivityLogRepository(ApplicationDbContext context, ILogger logger) :IActivityLogRepository
{

}
