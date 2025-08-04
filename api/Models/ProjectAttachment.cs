using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Enums;
using Microsoft.EntityFrameworkCore;

namespace api.Models;

public class ProjectAttachment
{


    [Key]
    public int Id { get; set; }

    [Required]
    public Guid  ResourceId { get; set; }
    [ForeignKey(nameof(ResourceId))]
    public Resource Resource { get; set; }


    [Required]
    public Guid  ProjectId { get; set; }
    [ForeignKey(nameof(ProjectId))]
    public Project Project { get; set; }

    public UserResourceLocation ResourceLocation { get; set; }
    public DateTime LastDownloadAt { get; set; }


    [Required]
    public Guid? AttachedByUserId { get; set; }
    [ForeignKey(nameof(AttachedByUserId))]

    public User? AttachedBy { get; set; }


    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.UtcNow;


    public static void ConfigureRelations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectAttachment>()
            .HasOne(ugr => ugr.Resource)
            .WithMany(r => r.ProjectAttachments)
            .HasForeignKey(ugr => ugr.ResourceId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ProjectAttachment>()
            .HasOne(pr => pr.Project)
            .WithMany(w => w.ProjectAttachments)
            .HasForeignKey(ugr => ugr.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);




        modelBuilder.Entity<ProjectAttachment>()
            .HasOne(iug => iug.AttachedBy)
            .WithMany(ug => ug.CreatedProjectAttachments)
            .HasForeignKey(iug => iug.AttachedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
