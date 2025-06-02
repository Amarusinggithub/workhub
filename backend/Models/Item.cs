namespace backend.Models;

public abstract class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Type Type { get; set; }
    public  string Location { get; set; }
    public DateTime ModifiedAt { get; set; }
    public DateTime Uploaded { get; set; }
    public bool IsShared { get; set; } = false;

}
