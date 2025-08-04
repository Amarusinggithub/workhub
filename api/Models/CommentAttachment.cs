using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.Models;

public class CommentAttachment
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();

    [Required] public Guid CommentId { get; set; }

    [ForeignKey(nameof(CommentId))] public Comment Comment { get; set; } = null!;

    [Required] public Guid AttactmentId { get; set; }
    [ForeignKey(nameof(AttactmentId))] public Resource Attactment { get; set; } = null!;

    [Required] public Guid? AttachedByUserId { get; set; }
    [ForeignKey(nameof(AttachedByUserId))] public User? AttachedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    public static void ConfigureRelations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CommentAttachment>()
            .HasOne(iug => iug.Comment)
            .WithMany(ug => ug.Attachments)
            .HasForeignKey(iug => iug.CommentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CommentAttachment>()
            .HasOne(iug => iug.Attactment)
            .WithMany(ug => ug.CommentAttachments)
            .HasForeignKey(iug => iug.CommentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CommentAttachment>()
            .HasOne(iug => iug.AttachedBy)
            .WithMany(ug => ug.CreatedCommentAttachments)
            .HasForeignKey(iug => iug.AttachedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

    }

}
