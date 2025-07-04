using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

public class NotificationMember
{
    [Key]
    public int Id { get; set; }


    [Required]
    public User User { get; set; }
    public int UserId { get; set; }


    [Required]
    public Notification Notification { get; set; }
    public int NotificationId { get; set; }




}
