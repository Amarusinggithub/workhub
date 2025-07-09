using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Auth;
public class RegisterRequestDto
{
    [Required]
    public string firstName { get; set; } = null!;

    [Required]
    public string lastName { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string email { get; set; } = null!;

    [Required]
    [MinLength(12)]
    public string password { get; set; } = null!;
}

