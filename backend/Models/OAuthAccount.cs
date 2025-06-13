using System.ComponentModel.DataAnnotations;
using backend.Enums;
namespace backend.Models;

public class OAuthAccount
{



    [Key]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }
    public User User { get; set; }

    public OAuthProvider OAuthProvider { get; set; }
    public string ProviderUrl { get; set; }
    public string RedirectUrl { get; set; }

    public string? OAuthProviderUserId { get; set; }
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? ExpiresAt { get; set; }




}
