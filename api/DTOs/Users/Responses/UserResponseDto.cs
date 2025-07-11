using System.ComponentModel.DataAnnotations;
using api.DTOs.Auth;

namespace api.DTOs.Users.Responses;

public class UserResponseDto
{
    [Required]
    public string firstName { get; set; }

    [Required]
    public string lastName { get; set; }

    [Required]
    [EmailAddress]
    public string email { get; set; }

    public string? profilePicture { get; set; }
    public string? headerImage { get; set; }
    public string? jobTItle { get; set; }
    public string?  organization { get; set; }
    public  string? location { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime? lastLoggedIn { get; set; }
    public bool? isActive { get; set; }

    [Required]
   public AuthTokenResponse AuthTokens { get; set; }

}
