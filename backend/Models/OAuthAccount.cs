using System.ComponentModel.DataAnnotations;
using backend.Enums;
namespace backend.Models;

public class OAuthAccount
{



    [Key]
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; }
    public User User { get; set; }

    public OAuthProvider OAuthProvider { get; set; }
    public string? ProviderUserId { get; set; }
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? ExpiresAt { get; set; }




}
