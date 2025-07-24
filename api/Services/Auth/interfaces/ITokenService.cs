using api.DTOs.Auth;
using api.Models;

namespace api.Services.Auth.interfaces;

public interface ITokenService
{
    public Task<(string jwtToken, DateTime expiresAtUtc)>  GenerateToken(User user);
    string GenerateRefreshToken();
    public Task<(string refreshToken, DateTime expiresAtUtc)>  GenerateAndSaveRefreshTokenAsync(User user);


    public void WriteAuthTokenAsHttpOnlyCookie(string cookieName, string token,
        DateTime expiration);

}
