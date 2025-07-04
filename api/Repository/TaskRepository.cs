using api.Data;
using api.Repositorys.interfaces;
using Microsoft.EntityFrameworkCore;
using Task = api.Models.Task;

namespace api.Repository;

public class TaskRepository: GenericRepository<Task>,ITaskRepository
{
    public TaskRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
    {
    }

    public override async Task<IEnumerable<Task>> GetAll()
    {
        try
        {
            return await dbSet.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e,"{Repo} All method error ",typeof(WorkSpaceRepository));
            return new List<Task>();
        }

    }

    public override async Task<bool> Upsert(Task entity)
    {
        try
        {
            var existingUser = await dbSet.Where(x => x.Id == entity.Id)
                .FirstOrDefaultAsync();

            if(existingUser==null)
                return await Add(entity);
            return true;

        }
        catch (Exception e)
        {
            _logger.LogError(e,"{Repo} Upsert method error ",typeof(TaskRepository));
            return false;
        }    }


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
            _logger.LogError(e,"{Repo} Delete method error ",typeof(TaskRepository));
            return false;
        }    }
}
