using api.Data;
using api.Repository.Subscription.interfaces;

namespace api.Repository.Subscription;

public class UsageRecordRepository(ApplicationDbContext context, ILogger logger) :IUsageRecordRepository
{

}
