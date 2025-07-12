using api.Data;
using api.Repository.Boards.interfaces;

namespace api.Repository.Boards;

public class BoardViewRepository(ApplicationDbContext context, ILogger logger) :IBoardViewRepository
{

}
