using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Auth;
public class RegisterRequestDto
{
    [Required]
    public string firstName { get; set; }

    [Required]
    public string lastName { get; set; }

    [Required]
    [EmailAddress]
    public string email { get; set; }

    [Required]
    [MinLength(12)]
    public string password { get; set; }
}

