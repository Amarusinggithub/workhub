using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.Models;

public class UserSession
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid UserId { get; set; }
    [ForeignKey(nameof(UserId))]

    public User User { get; set; } = null!;

    [Required]
    [StringLength(100)]
    public string SessionToken { get; set; } = string.Empty;

    [StringLength(45)]
    public string? IpAddress { get; set; }

    [StringLength(500)]
    public string? UserAgent { get; set; }

    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.UtcNow;    public DateTime ExpiresAt { get; set; }
    public DateTime? LastActivityAt { get; set; }
    public bool IsActive { get; set; } = true;


    public static void ConfigureRelations(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<UserSession>()
            .HasOne(iug => iug.User)
            .WithMany(ug => ug.Sessions)
            .HasForeignKey(iug => iug.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
