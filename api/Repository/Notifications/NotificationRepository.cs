using api.Data;
using api.Models;
using api.Repository.interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repository.Notifications;

public class NotificationRepository:GenericRepository<Notification,Guid>,INotificationRepository
{
    public NotificationRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
    {
    }

    public override  async Task<IEnumerable<Notification>> GetAll()
    {
        try
        {
            return await dbSet.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e,"{Repo} All method error ",typeof(NotificationRepository));
            return new List<Notification>();
        }
    }


    public override async Task<bool> Upsert(Notification entity)
    {
        try
        {
            var exitingUser = await dbSet.Where(x => x.Id == entity.Id)
                .FirstOrDefaultAsync();

            if (exitingUser == null)
                await Add(entity);
            return true;


        }
        catch (Exception e)
        {
            _logger.LogError(e,"{Repo} Upsert method error ",typeof(NotificationRepository));
            return false;
        }

    }

    public override async Task<bool> Delete(Guid id)
    {
        try
        {
            var exitingUser = await dbSet.Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (exitingUser != null)
            {
                dbSet.Remove(exitingUser);
                return true;
            }
            return false;


        }
        catch (Exception e)
        {
            _logger.LogError(e,"{Repo} Delete method error ",typeof(NotificationRepository));
            return false;
        }    }
}
