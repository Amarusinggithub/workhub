using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class UserNotification
{
    [Key]
    public int id { get; set; }


    [Required]
    public User User { get; set; }
    public string Userid { get; set; }


    [Required]
    public Notification Notification { get; set; }
    public int Notificationid { get; set; }


}
