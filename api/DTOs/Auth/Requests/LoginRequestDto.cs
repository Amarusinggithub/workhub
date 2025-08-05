using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Auth.Requests;

public class LoginRequestDto
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    public string password { get; set; } = string.Empty;
}
