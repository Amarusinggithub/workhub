using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.Models;

public class CommentMention
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid CommentId { get; set; }

    [ForeignKey(nameof(CommentId))]

    public Comment Comment { get; set; } = null!;

    [Required]
    public Guid MentionedUserId { get; set; }
    [ForeignKey(nameof(MentionedUserId))]

    public User MentionedUser { get; set; } = null!;

    public bool IsNotified { get; set; } = false;
    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.UtcNow;


    public static void ConfigureRelations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CommentMention>()
            .HasOne(iug => iug.Comment)
            .WithMany(ug => ug.Mentions)
            .HasForeignKey(iug => iug.CommentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CommentMention>()
            .HasOne(iug => iug.MentionedUser)
            .WithMany()
            .HasForeignKey(iug => iug.MentionedUserId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
