using api.Data;
using api.Repository.Infrastructure.interfaces;

namespace api.Repository.Infrastructure;

public class CacheRepository(ApplicationDbContext context, ILogger logger) :ICacheRepository
{

}
