using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Enums;
using Microsoft.EntityFrameworkCore;

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

    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.UtcNow;    public DateTime UploadedAt { get; set; }=DateTime.UtcNow;
    public bool IsDeleted { get; set; }

    public long ResourceSize { get; set; }


    [Required]
    public  ResourceType  ResourceType{ get; set; }

    [Required]
    public Guid UploaderId { get; set; }
    [ForeignKey(nameof(UploaderId))]

    public User Uploader { get; set; }


    [Required]
    public Guid DeletedById  { get; set; }
    [ForeignKey(nameof(DeletedById))]

    public User DeletedBy { get; set; }


    public ICollection<ProjectAttachment> ProjectAttachments { get; set; } = new List<ProjectAttachment>();
    public ICollection<TaskAttachment> TaskAttachments { get; set; } = new List<TaskAttachment>();
    public ICollection<CommentAttachment> CommentAttachments { get; set; } = new List<CommentAttachment>();



    public void SoftDelete(Guid deleterId)
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
        DeletedById = deleterId;
        UpdatedAt = DateTime.UtcNow;
    }

    public static void ConfigureRelations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Resource>()
            .HasOne(r => r.Uploader)
            .WithMany(u => u.UploadedResources)
            .HasForeignKey(r => r.UploaderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Resource>()
            .HasOne(r => r.DeletedBy)
            .WithMany(u => u.DeletedResources)
            .HasForeignKey(r => r.DeletedById)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
