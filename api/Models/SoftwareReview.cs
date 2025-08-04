using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.Models;

public class SoftwareReview
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int SoftwareId { get; set; }
    [ForeignKey(nameof(SoftwareId))]

    public Software Software { get; set; }

    [Required]
    public Guid UserId { get; set; }
    [ForeignKey(nameof(UserId))]

    public User User { get; set; }

    [Range(1, 5)]
    public int Rating { get; set; }

    [MaxLength(1000)]
    public string? ReviewText { get; set; }

    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.UtcNow;
    public bool IsApproved { get; set; } = false;

    public bool IsPublic { get; set; } = true;

    public static void ConfigureRelations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SoftwareReview>()
            .HasOne(iug => iug.Software)
            .WithMany(ug => ug.Reviews)
            .HasForeignKey(iug => iug.SoftwareId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
