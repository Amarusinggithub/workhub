using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Enums;
using Microsoft.EntityFrameworkCore;

namespace api.Models;

public class Prerequisite
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int OfferId { get; set; }
    [ForeignKey(nameof(OfferId))]

    public Offer Offer { get; set; }

    [Required]
    public int PlanId { get; set; }
    [ForeignKey(nameof(PlanId))]


    public Plan Plan { get; set; }

    public PrerequisiteType Type { get; set; } = PrerequisiteType.Required;
    public int MinimumMonths { get; set; } = 0;
    public bool IsActive { get; set; } = true;

    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.UtcNow;

    public static void ConfigureRelations(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Prerequisite>()
            .HasOne(pre => pre.Offer)
            .WithMany(o => o.Prerequisites)
            .HasForeignKey(pre => pre.OfferId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Prerequisite>()
            .HasOne(pre => pre.Plan)
            .WithMany(p => p.Prerequisites)
            .HasForeignKey(pre => pre.PlanId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}


