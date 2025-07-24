using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
namespace api.Models;

public class User: IdentityUser<int>
{
    [Required]
    public string Email { get; set; }

   public string FirstName { get; set; }
   public string LastName { get; set; }
    public string? AvatarUrl { get; set; }
    public string? HeaderImage { get; set; }
    public string? JobTitle { get; set; }
    public string?  Organization { get; set; }
    public  string? Location { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiresAtUtc { get; set; }
    public DateTime? RefreshTokenExpiryDate { get; set; }

    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLoggedIn { get; set; }
    public DateTime? PasswordLastChanged { get; set; }
    public bool? IsActive { get; set; }

   /* [Required]
    public int InUserGroupId { get; set; }
    public InUserGroup Group { get; set; }*/


    [Required]
    public ICollection<TaskItem> Issues { get; set; } = new List<TaskItem>();
    public ICollection<UserGroupMember> UserGroups { get; set; } = new List<UserGroupMember>();

    public ICollection<Comment> Comments { get; set; } = new List<Comment>();


    public ICollection<ProjectMember> UserProjects { get; set; } = new List<ProjectMember>();

    [Required]
    public ICollection<UserWorkspaceRole> UserWorkspaceRoles { get; set; } = new List<UserWorkspaceRole>();

    // ToDo: add id For notification if not many to many relation or  make join table for user and group
    public ICollection<NotificationMember> UserNotifications { get; set; } = new List<NotificationMember>();
    public ICollection<ProjectResource> UserResources { get; set; } = new List<ProjectResource>();
    public ICollection<OAuthAccount> OAuthAccounts { get; set; } = new List<OAuthAccount>();
    public ICollection<WorkSpaceMember> UserWorkSpaces { get; set; } = new List<WorkSpaceMember>();

    public override string ToString()
    {
        return FirstName + " " + LastName;
    }
}
