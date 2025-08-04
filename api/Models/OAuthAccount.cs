using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Enums;

namespace api.Models;

public class OAuthAccount
{

    [Key]
    public Guid Id { get; set; } =  Guid.NewGuid();

    [Required]
    public Guid  UserId { get; set; }
    [ForeignKey(nameof(UserId))]

    public User User { get; set; }

    public OAuthProvider OAuthProvider { get; set; }
    public string ProviderUrl { get; set; }
    public string RedirectUrl { get; set; }

    public string? OAuthProviderUserId { get; set; }
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? ExpiresAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.UtcNow;

    public DateTime? LastSyncAt { get; set; }


}
