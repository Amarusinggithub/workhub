using api.DTOs.Auth;
using api.Models;

namespace api.Services.Auth.interfaces;

public interface ITokenService
{
    public Task<string> GenerateToken(User user);
    string GenerateRefreshToken();
    public Task<string> GenerateAndSaveRefreshTokenAsync(User user);
    public Task<TokenResponseDto?> RefreshTokenAsync(string refreshToken, int userId);
    public Task<bool> ValidateRefreshTokenAsync(string refreshToken, int userId);
    public int? GetUserIdFromToken(string token);
    public Task<bool> RevokeRefreshTokenAsync(int userId);



}
