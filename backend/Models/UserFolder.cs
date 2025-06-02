using System.ComponentModel.DataAnnotations;
using backend.Enums;
namespace backend.Models;

public class UserFolder
{
    [Key]
    public int Id { get; set; }
    public User User { get; set; }
    public int UserId { get; set; }
    public Folder Folder { get; set; }
    public  Location FolderLocation { get; set; }
    public int FolderId { get; set; }
    public Role UserRole { get; set; }

}


