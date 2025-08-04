using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.Models;

public class Include
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

    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.UtcNow;


    public static void ConfigureRelations(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Include>()
            .HasOne(inc => inc.Offer)
            .WithMany(o => o.Includes)
            .HasForeignKey(inc => inc.OfferId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Include>()
            .HasOne(inc => inc.Plan)
            .WithMany(p => p.Includes)
            .HasForeignKey(inc => inc.PlanId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
