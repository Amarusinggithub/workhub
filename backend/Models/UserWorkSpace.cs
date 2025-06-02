using System.ComponentModel.DataAnnotations;
using backend.Enums;
namespace backend.Models;

public class UserWorkSpace
{
    [Key]
    public int Id { get; set; }
    public int GroupId { get; set; }
    public WorkSpace WorkSpace { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public DateTime AddedAt { get; set; }
    public DateTime RemovedAt { get; set; }
    public Role  Role { get; set; }







}
