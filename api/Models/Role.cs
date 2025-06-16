using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace api.Models;

public class Role: IdentityRole<int>
{
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }

    public bool IsDeleted { get; set; }

    [Required]

    public ICollection<WorkSpaceMember> WorkSpaceRoles { get; set; } = new List<WorkSpaceMember>();
    //public ICollection<UserGroupResource> ResourceRoles { get; set; } = new List<UserGroupResource>();
    public ICollection<UserProjectRole> ProjectRoles { get; set; } = new List<UserProjectRole>();
}
