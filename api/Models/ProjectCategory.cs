using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.Models;

public class ProjectCategory
{
    [Key]
    public int Id { get; set; }

    [Required]
    public Guid  ProjectId { get; set; }
    [ForeignKey(nameof(ProjectId))]

    public Project Project { get; set; }

    [Required]
    public int CategoryId { get; set; }
    [ForeignKey(nameof(CategoryId))]

    public Category Category { get; set; }

    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.UtcNow;

    public static void ConfigureRelations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectCategory>()
            .HasOne(pc => pc.Project)
            .WithMany(p => p.ProjectCategories)
            .HasForeignKey(pc => pc.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ProjectCategory>()
            .HasOne(pc => pc.Category)
            .WithMany(c => c.ProjectCategories)
            .HasForeignKey(pc => pc.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
