using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using backend.Enums;
namespace backend.Models;

public class User: IdentityUser
{
   // [Key]
   // public int Id { get; set; }
    public string ProfilePicture { get; set; }
    public string HeaderImage { get; set; }
    public string JobTItle { get; set; }
    public string  Organization { get; set; }
    public  string Location { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLoggedIn { get; set; }
    public DateTime? PasswordLastChanged { get; set; }
    public bool? IsActive { get; set; }

   /* [Required]
    public int InUserGroupId { get; set; }
    public InUserGroup Group { get; set; }*/


    [Required]
    public ICollection<Task> Tasks { get; set; } = new List<Task>();
    public ICollection<InUserGroup> UserGroups { get; set; } = new List<InUserGroup>();

    public ICollection<InProject> UserProjects { get; set; } = new List<InProject>();

    [Required]
    public ICollection<UserProjectRole> UserProjectRoles { get; set; } = new List<UserProjectRole>();

    // ToDo: add id For notification if not many to many relation or  make join table for user and group
    public ICollection<UserNotification> UserNotifications { get; set; } = new List<UserNotification>();
    public ICollection<UserGroupResource> UserResources { get; set; } = new List<UserGroupResource>();
    public ICollection<OAuthAccount> OAuthAccounts { get; set; } = new List<OAuthAccount>();
    public ICollection<InWorkSpace> UserWorkSpaces { get; set; } = new List<InWorkSpace>();
}
