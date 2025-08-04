using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace api.Models;

public class Role: IdentityRole<Guid>
{


    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.UtcNow;

    public bool IsDeleted { get; set; }
    public DateTime DeletedAt { get; set; }


    [Required]

    public ICollection<WorkspaceRole> WorkspaceRoles { get; set; } = new List<WorkspaceRole>();
}
