using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Auth.Requests;

public class LoginRequestDto
{
    [Required]
    [EmailAddress]
    public string email { get; set; }

    [Required]
    public string password { get; set; }
}
