using api.Data;
using api.Models;
using api.Repository.Projects.interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repository.Projects;

public class ProjectRepository: GenericRepository<Project>,IProjectRepository
{
    public ProjectRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
    {
    }

    public override  async Task<IEnumerable<Project>> GetAll()
    {
        try
        {
            return await dbSet.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e,"{Repo} All method error ",typeof(ProjectRepository));
            return new List<Project>();
        }
    }


    public override async Task<bool> Upsert(Project entity)
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
            _logger.LogError(e,"{Repo} Upsert method error ",typeof(ProjectRepository));
            return false;
        }

    }

    public override async Task<bool> Delete(int id)
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
            _logger.LogError(e,"{Repo} Delete method error ",typeof(ProjectRepository));
            return false;
        }    }
}
