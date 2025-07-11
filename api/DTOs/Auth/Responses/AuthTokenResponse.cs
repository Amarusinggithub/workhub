namespace api.DTOs.Auth;

public class AuthTokenResponse
{
    public required string RefreshToken { get; set; }
    public required string AccessToken { get; set; }

}
