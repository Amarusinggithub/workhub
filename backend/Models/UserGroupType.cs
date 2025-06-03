using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class UserGroupType
{

    [Key]
    public int Id { get; set; }

    [Required]
    public string TypeName { get; set; } = string.Empty;

    public int MembersMin { get; set; }
    public int MembersMax { get; set; }

    public ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
    public ICollection<Plan> Plans { get; set; } = new List<Plan>();



}
