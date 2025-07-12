using api.Data;
using api.Repository.Notifications.interfaces;

namespace api.Repository.Notifications;

public class UserNotificationRepository(ApplicationDbContext context, ILogger logger) :IUserNotificationRepository
{

}
