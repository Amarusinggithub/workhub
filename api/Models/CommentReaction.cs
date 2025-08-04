using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.Models;

public class CommentReaction
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid CommentId { get; set; }
    [ForeignKey(nameof(CommentId))]

    public Comment Comment { get; set; } = null!;

    [Required]
    public Guid UserId { get; set; }
    [ForeignKey(nameof(UserId))]

    public User User { get; set; } = null!;

    [Required]
    [StringLength(20)]
    public string ReactionType { get; set; } = string.Empty; // Like, Dislike, Love, etc.

    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.UtcNow;

    public static void ConfigureRelations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CommentReaction>()
            .HasOne(iug => iug.Comment)
            .WithMany(ug => ug.Reactions)
            .HasForeignKey(iug => iug.CommentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CommentReaction>()
            .HasOne(iug => iug.User)
            .WithMany()
            .HasForeignKey(iug => iug.UserId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
