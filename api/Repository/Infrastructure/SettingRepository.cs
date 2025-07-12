using api.Data;
using api.Repository.Infrastructure.interfaces;

namespace api.Repository.Infrastructure;

public class SettingRepository(ApplicationDbContext context, ILogger logger) :ISettingRepository
{

}
