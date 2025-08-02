using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace api.Models;

public class Role: IdentityRole<Guid>
{


    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }

    public bool IsDeleted { get; set; }

    [Required]

    public ICollection<WorkSpaceMember> WorkSpaceMembers { get; set; } = new List<WorkSpaceMember>();
    //public ICollection<UserGroupResource> ResourceRoles { get; set; } = new List<UserGroupResource>();
    public ICollection<WorkspaceRole> UserWorkspaceRoles { get; set; } = new List<WorkspaceRole>();
}
