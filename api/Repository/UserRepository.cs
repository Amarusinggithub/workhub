using api.Database;
using api.Models;
using api.Repositorys.interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class UserRepository(ApplicationDbContext context, ILogger logger)
    : GenericRepository<User>(context, logger), IUserRepository
{
    public override async Task<IEnumerable<User>> GetAll()
    {
        try
        {
            return await dbSet.Where(x => x.IsActive == true).ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e,"{Repo} All method error ",typeof(UserRepository));
            return new List<User>();
        }
    }

    public async Task<User> Authenticate(string password,string email)
    {


        var newUser = await dbSet.SingleOrDefaultAsync(x => x.Email == email && x.PasswordHash == password);

        if (newUser == null) return null;

        // authentication successful so generate jwt token
        //var token = await generateJwtToken(newUser);

        return newUser;
    }

    public async Task<User?> GetByEmail(string email)
    {
        var newUser = await dbSet.SingleOrDefaultAsync(x => x.Email == email );
        return newUser;


    }


    public override async Task<bool> Upsert(User entity)
    {
        try
        {
            var existingUser = await dbSet.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();

            if (existingUser == null)
                return await Add(entity);
            existingUser.FirstName = entity.FirstName;
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
