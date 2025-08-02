namespace api.Models;

public class BoardColumn
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Order { get; set; }

    public Guid BoardId { get; set; }
    public Board Board { get; set; } = null!;
}

