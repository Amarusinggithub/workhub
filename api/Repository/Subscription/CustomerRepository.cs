using api.Data;
using api.Repository.Subscription.interfaces;

namespace api.Repository.Subscription;

public class CustomerRepository(ApplicationDbContext context, ILogger logger) :ICustomerRepository
{

}
