using api.DTOs.Auth;
using api.Models;

namespace api.Services.Auth.interfaces;

public interface ITokenService
{
    public Task<(string jwtToken, DateTime expiresAtUtc)>  GenerateToken(User user);
    string GenerateRefreshToken();
    public Task<(string refreshToken, DateTime expiresAtUtc)>  GenerateAndSaveRefreshTokenAsync(User user);
    public Task RefreshTokenAsync(string refreshToken, Guid userId);
    public Task<bool> ValidateRefreshTokenAsync(string refreshToken, Guid userId);
    public int? GetUserIdFromToken(string token);
    public Task<bool> RevokeRefreshTokenAsync(Guid userId);

    public void WriteAuthTokenAsHttpOnlyCookie(string cookieName, string token,
        DateTime expiration);

}
