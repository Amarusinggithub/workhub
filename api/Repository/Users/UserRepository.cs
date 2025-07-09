using api.Data;
using api.Models;
using api.Repositories.interfaces;
using api.Repository.Users.interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repository.Users;

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

    public async Task<User?> AddAndUpdateUser(User? userObj)
    {
        bool isSuccess = false;
        if (userObj != null && userObj.Id > 0)
        {
            var obj = await _context.Users.FirstOrDefaultAsync(c => c.Id == userObj.Id);
            if (obj != null)
            {
                // obj.Address = userObj.Address;
                obj.FirstName = userObj.FirstName;
                obj.LastName = userObj.LastName;
                _context.Users.Update(obj);
                isSuccess = await _context.SaveChangesAsync() > 0;
            }
        }
        else
        {
            await _context.Users.AddAsync(userObj);
            isSuccess = await _context.SaveChangesAsync() > 0;
        }

        return isSuccess ? userObj: null;
    }

    public override async Task<User?> GetById(int id)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x != null && x.Id == id);
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
