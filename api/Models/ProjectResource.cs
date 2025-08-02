using System.ComponentModel.DataAnnotations;
using api.Enums;

namespace api.Models;

public class ProjectResource
{


    [Key]
    public int Id { get; set; }

    [Required]
    public Guid  ResourceId { get; set; }
    public Resource Resource { get; set; }

    [Required]
    public Guid  WorkspaceId { get; set; }
    public Workspace Workspace { get; set; }


    [Required]
    public Guid  ProjectId { get; set; }
    public Project Project { get; set; }

    public UserResourceLocation ResourceLocation { get; set; }
    public DateTime LastDownloadAt { get; set; }




}
