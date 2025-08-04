using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.Models;

public class SoftwareFeature
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int SoftwareId { get; set; }

    [ForeignKey(nameof(SoftwareId))]

    public Software Software { get; set; }

    [Required]
    [MaxLength(100)]
    public string FeatureName { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }

    [MaxLength(50)]
    public string? Icon { get; set; }

    public bool IsHighlight { get; set; } = false;

    public int SortOrder { get; set; } = 0;

    public bool IsActive { get; set; } = true;


    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.UtcNow;

    public static void ConfigureRelations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SoftwareFeature>()
            .HasOne(iug => iug.Software)
            .WithMany(ug => ug.SoftwareFeatures)
            .HasForeignKey(iug => iug.SoftwareId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
