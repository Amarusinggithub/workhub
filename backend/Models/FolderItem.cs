using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class FolderItem
{
    [Key]
    public int Id { get; set; }
    public Folder Folder { get; set; }
    public int FolderId { get; set; }
    public Item Item { get; set; }
    public int FileId { get; set; }
    public DateTime AddedAt { get; set; }
}
