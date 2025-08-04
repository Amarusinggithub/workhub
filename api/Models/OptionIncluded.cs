using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.Models;

public class OptionIncluded
{


    [Key]
    public int Id { get; set; }

    [Required]
    public int PlanId { get; set; }

    [ForeignKey(nameof(PlanId))]

    public Plan Plan { get; set; }

    [Required]
    public int OptionId { get; set; }
    [ForeignKey(nameof(OptionId))]

    public Option Option { get; set; }

    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; }



    public static void ConfigureRelations(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<OptionIncluded>()
            .HasOne(oi => oi.Plan)
            .WithMany(p => p.OptionIncludes)
            .HasForeignKey(oi => oi.PlanId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OptionIncluded>()
            .HasOne(oi => oi.Option)
            .WithMany(o => o.OptionIncludes)
            .HasForeignKey(oi => oi.OptionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
