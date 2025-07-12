using api.Data;
using api.Repository.Boards.interfaces;

namespace api.Repository.Boards;

public class BoardColumnRepository(ApplicationDbContext context, ILogger logger) :IBoardColumnRepository
{

}
