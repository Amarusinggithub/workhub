using api.Data;
using api.Repository.Subscription.interfaces;

namespace api.Repository.Subscription;

public class SubscriptionRepository:GenericRepository<Models.Subscription>,ISubscriptionRepository
{
    public SubscriptionRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
    {
    }
}
