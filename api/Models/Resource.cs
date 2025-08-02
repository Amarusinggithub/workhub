using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Enums;

namespace api.Models;

public class Resource
{
    [Key]
    public Guid  Id { get; set; }

    [Required]
    [StringLength(255)]
    public string FileName { get; set; } = string.Empty;

    [Required]
    [StringLength(500)]
    public string FilePath { get; set; } = string.Empty;

    [StringLength(100)]
    public string? ContentType { get; set; }

    public long FileSize { get; set; }

    public DateTime DeletedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
    public DateTime UploadedAt { get; set; }
    public bool IsDeleted { get; set; } = false;

    public long ResourceSize { get; set; }


    [Required]
    public  ResourceType  ResourceType{ get; set; }

    [Required]
    public Guid UploaderId { get; set; }
    [ForeignKey("UploaderId")]

    public User Uploader { get; set; }


    [Required]
    public Guid DeletedById  { get; set; }
    [ForeignKey("DeletedById ")]

    public User DeletedBy { get; set; }


    public ICollection<ProjectResource> UserResources { get; set; } = new List<ProjectResource>();
    public ICollection<TaskAttachment> TaskResources { get; set; } = new List<TaskAttachment>();
    public ICollection<CommentAttachment> CommentAttachments { get; set; } = new List<CommentAttachment>();


    public void SoftDelete(Guid deleterId)
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
        DeletedById = deleterId;
        UpdatedAt = DateTime.UtcNow;
    }
}
