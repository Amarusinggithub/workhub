using api.Data;
using api.Repository.Analytics.interfaces;

namespace api.Repository.Analytics;

public class DashboardRepository(ApplicationDbContext context, ILogger logger) :IDashboardRepository
{

}
