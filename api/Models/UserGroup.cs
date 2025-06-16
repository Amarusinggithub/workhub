using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class UserGroup
{

    [Key]
    public int Id { get; set; }

    [Required]
    public int UserGroupTypeId { get; set; }
    public UserGroupType UserGroupType { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    public ICollection<UserGroupResource> Resources { get; set; } = new List<UserGroupResource>();
    public ICollection<UserGroupMember> Users { get; set; } = new List<UserGroupMember>();
}
