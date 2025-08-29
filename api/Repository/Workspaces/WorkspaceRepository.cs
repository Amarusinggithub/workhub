using api.Data;
using api.Models;
using api.Repositories.interfaces;
using api.Repository.Workspaces.interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repository.Workspaces;

public class WorkSpaceRepository : GenericRepository<Workspace,Guid>,IWorkSpaceRepository
{
    public WorkSpaceRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
    {

    }

    public override async Task<IEnumerable<Workspace>> GetAll()
    {
        try
        {
            return await dbSet.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e,"{Repo} All method error ",typeof(WorkSpaceRepository));
            return new List<Workspace>();
        }
    }

    public override async Task<bool> Upsert(Workspace entity)
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


    public override async Task<bool> Delete(Guid id)
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
