using System.ComponentModel.DataAnnotations;
using api.Enums;

namespace api.Models;

public class Resource
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string ResourceName { get; set; } = string.Empty;


    [Required]
    public string S3Url { get; set; } = string.Empty;

    public DateTime ModifiedAt { get; set; }
    public DateTime UploadedAt { get; set; }

    public bool IsShared { get; set; } = false;
    public long ResourceSize { get; set; }

    [Required]
    public int UserGroupId { get; set; }
    public UserGroup UserGroup { get; set; }

    [Required]
    public  ResourceType  ResourceType{ get; set; }

    [Required]
    public int UploaderId { get; set; }
    public User Uploader { get; set; }


   /* public int? TaskId { get; set; }
    public Task? Task { get; set; }

    public int? ProjectId { get; set; }
    public Project? Project { get; set; }

    public int? WorkSpaceId { get; set; }
    public WorkSpace? WorkSpace { get; set; }*/

    public ICollection<ProjectResource> UserResources { get; set; } = new List<ProjectResource>();
    public ICollection<TaskResource> TaskResources { get; set; } = new List<TaskResource>();

}
