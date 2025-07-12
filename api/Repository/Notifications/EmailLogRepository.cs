using api.Data;
using api.Repository.Notifications.interfaces;

namespace api.Repository.Notifications;

public class EmailLogRepository(ApplicationDbContext context, ILogger logger) :IEmailLogRepository
{

}
