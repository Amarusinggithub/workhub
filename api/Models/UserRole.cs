using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.Models;

public class UserRole
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid UserId { get; set; }
    [ForeignKey(nameof(UserId))]


    public User User { get; set; } = null!;

    [Required]
    public int? RoleId { get; set; }
    [ForeignKey(nameof(RoleId))]

    public Role Role { get; set; }

    [StringLength(200)]
    public string? Description { get; set; }

    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.UtcNow;
    public DateTime? ExpiresAt { get; set; }


    public static void ConfigureRelations(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<UserRole>()
            .HasOne(iug => iug.User)
            .WithMany(ug => ug.CustomRoles)
            .HasForeignKey(iug => iug.UserId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
