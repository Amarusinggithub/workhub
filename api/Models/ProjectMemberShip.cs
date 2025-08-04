using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.Models;

public class ProjectMemberShip
{



    [Key]
    public int Id { get; set; }

    [Required]
    public Guid  UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public User User { get; set; }

    [Required]
    public Guid  ProjectId { get; set; }
    [ForeignKey(nameof(ProjectId))]

    public Project Project { get; set; }

    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; }

    [Required]
    public Guid  AddedByUserId { get; set; }
    [ForeignKey(nameof(AddedByUserId))]

    public User AddedBy { get; set; }

    public static void ConfigureRelations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectMemberShip>()
            .HasOne(inp => inp.Project)
            .WithMany(p => p.ProjectMemberShips)
            .HasForeignKey(inp => inp.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ProjectMemberShip>()
            .HasOne(inp => inp.User)
            .WithMany(u => u.ProjectMemberShips)
            .HasForeignKey(inp => inp.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ProjectMemberShip>()
            .HasOne(inp => inp.AddedBy)
            .WithMany()
            .HasForeignKey(inp => inp.AddedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

    }

}
