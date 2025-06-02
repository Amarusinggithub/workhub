using System.ComponentModel.DataAnnotations;
using backend.Enums;

namespace backend.Models;

public class UserFile
{
    [Key]
    public int Id { get; set; }
    public File File { get; set; }
    public int FileId { get; set; }
    public  User User { get; set; }
    public int UserId { get; set; }
    public  Location FileLocation { get; set; }
    public DateTime LastOpenAt { get; set; }
    public Boolean isTrashed { get; set; }
    public Boolean isFavorited { get; set; }
    public Role UserRole { get; set; }




}
