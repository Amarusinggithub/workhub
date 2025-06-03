using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class Role
{

    [Key]
    public int Id { get; set; }

    [Required]
    public string RoleName { get; set; } = string.Empty;

    public ICollection<InWorkSpace> WorkSpaceRoles { get; set; } = new List<InWorkSpace>();
    //public ICollection<UserGroupResource> ResourceRoles { get; set; } = new List<UserGroupResource>();
    public ICollection<UserProjectRole> ProjectRoles { get; set; } = new List<UserProjectRole>();
}
