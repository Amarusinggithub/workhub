using api.Data;
using api.Repository.Analytics.interfaces;

namespace api.Repository.Analytics;

public class ReportRepository(ApplicationDbContext context, ILogger logger) :IReportRepository
{

}
