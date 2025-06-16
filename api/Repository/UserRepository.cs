using api.Database;
using api.Models;
using api.Repositorys.interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class UserRepository:GenericRepository<User>,IUserRepository
{
    public UserRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
    {
    }

    public override async Task<IEnumerable<User>> GetAll()
    {
        try
        {
            return await dbSet.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e,"{Repo} All method error ",typeof(UserRepository));
            return new List<User>();
        }
    }

    public override async Task<bool> Upsert(User entity)
    {
        try
        {
            var existingUser = await dbSet.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();

            if (existingUser == null)
                return await Add(entity);
            existingUser.FirtName = entity.FirtName;
            existingUser.LastName = entity.LastName;
            existingUser.Email = entity.Email;

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e,"{Repo} Upsert method error ",typeof(UserRepository));
            return false;
        }

    }

    public override async Task<bool> Delete(int id)
    {
        try
        {
            var existingUser = await dbSet.Where(x => x.Id == id)
                .FirstOrDefaultAsync();


            if (existingUser != null)
            {
                dbSet.Remove(existingUser);
                return true;
            }

            return false;
        }
        catch (Exception e)
        {
            _logger.LogError(e,"{Repo} Delete method error ",typeof(UserRepository));
            return false;
        }

    }
}
