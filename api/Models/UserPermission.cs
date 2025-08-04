using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.Models;

public class UserPermission
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid UserId { get; set; }
    [ForeignKey(nameof(UserId))]

    public User User { get; set; } = null!;

    [Required]
    [StringLength(100)]
    public string Permission { get; set; } = string.Empty;

    [StringLength(100)]
    public string? Resource { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ExpiresAt { get; set; }

    public static void ConfigureRelations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserPermission>()
            .HasOne(iug => iug.User)
            .WithMany(ug => ug.CustomPermissions)
            .HasForeignKey(iug => iug.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
