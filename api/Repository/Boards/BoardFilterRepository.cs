using api.Data;
using api.Repository.Boards.interfaces;

namespace api.Repository.Boards;

public class BoardFilterRepository(ApplicationDbContext context, ILogger logger) :IBoardFilterRepository
{

}
