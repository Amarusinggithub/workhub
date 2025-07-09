using api.Data;
using api.Models;
using api.Repository.Infrastructure.interfaces;

namespace api.Repository.Software;

public class StorageRepository:GenericRepository<Resource>,IStorageRepository
{
    public StorageRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
    {
    }

    public Task<Resource> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Resource>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<bool> Add(Resource entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(Resource entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }
}
