using api.Data;
using api.Repository.Infrastructure.interfaces;

namespace api.Repository.Infrastructure;

public class WebhookRepository(ApplicationDbContext context, ILogger logger) :IWebhookRepository
{

}
