namespace api.Models;

public class Board
{
    public Guid Id { get; set; } =  Guid.NewGuid();
    public string Name { get; set; } = "Board";

    public Guid ProjectId { get; set; }
    public Project Project { get; set; } = null!;

    public ICollection<BoardColumn> Columns { get; set; } = new List<BoardColumn>();
}

