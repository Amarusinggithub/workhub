using api.Data;
using api.Models;
using api.Repository.Boards.interfaces;

namespace api.Repository.Boards;

public class BoardRepository:GenericRepository<Board,Guid>,IBoardRepository
{
    public BoardRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
    {
    }
}
