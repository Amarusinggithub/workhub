namespace api.DTOs.Auth;

public class TokenResponseDto
{
    public required string RefreshToken { get; set; }
    public required string AccessToken { get; set; }

}
