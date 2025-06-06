using backend.Database;
using backend.Models;
using backend.Repositorys.interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository;

public class WorkSpaceRepository : GenericRepository<WorkSpace>,IWorkSpaceRepository
{
    public WorkSpaceRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
    {

    }

    public override async Task<IEnumerable<WorkSpace>> GetAll()
    {
        try
        {
            return await dbSet.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e,"{Repo} All method error ",typeof(WorkSpaceRepository));
            return new List<WorkSpace>();
        }
    }

    public override async Task<bool> Upsert(WorkSpace entity)
    {
        try
        {
            var existingUser = await dbSet.Where(x => x.Id == entity.Id)
                .FirstOrDefaultAsync();

            if (existingUser == null)
                return await Add(entity);


            return true;

        }
        catch (Exception e)
        {
            _logger.LogError(e,"{Repo} Upsert method error ",typeof(WorkSpaceRepository));
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
            _logger.LogError(e,"{Repo} Delete method error ",typeof(WorkSpaceRepository));
            return false;
        }

    }
}
