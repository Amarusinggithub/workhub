using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class Notification
{



    /*public User NotifictionTo { get; set; }
    public int Userid { get; set; }
    public Project? Project { get; set; }
    public int? projectid { get; set; }

    public WorkSpace? WorkSpace { get; set; }
    public int? projectid { get; set; }*/

    [Key]
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? ScheduledAt { get; set; }
    public DateTime? SentAt { get; set; }



    public string? Message { get; set; }


    public ICollection<NotificationMember> UserNotifications { get; set; } = new List<NotificationMember>();

}
