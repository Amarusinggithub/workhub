using System.ComponentModel.DataAnnotations;

namespace api.Models;
public class RegisterRequest
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

