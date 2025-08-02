using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using api.Data.interfaces;
using api.DTOs.Auth;
using api.Models;
using api.Services.Auth.interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace api.Services.Auth;

public class TokenService(
    ILogger<TokenService> logger,
    IUnitOfWork unitOfWork,
    IConfiguration configuration,
    UserManager<User> userManager, IHttpContextAccessor httpContextAccessor) : ITokenService
{
    private readonly ILogger<TokenService> _logger = logger;
    private readonly UserManager<User> _userManager = userManager;
    private readonly IHttpContextAccessor _httpContextAccessor=httpContextAccessor;


    public async Task<(string jwtToken, DateTime expiresAtUtc)>  GenerateToken(User user)
    {
        _logger.LogInformation("Generating JWT token for user with ID: {UserId}", user.Id);

        try
        {
            var claims = new List<Claim>
            {

                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim("userId", user.Id.ToString()),
                new Claim("firstName", user.FirstName),
                new Claim("lastName", user.LastName ),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,
                    new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(),
                    ClaimValueTypes.Integer64)



            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var secretKey = configuration.GetValue<string>("AppSettings:Secret");
            var issuer = configuration.GetValue<string>("AppSettings:Issuer");
            var audience = configuration.GetValue<string>("AppSettings:Audience");

            if (string.IsNullOrEmpty(secretKey))
            {
                _logger.LogError("JWT Secret key is not configured");
                throw new InvalidOperationException("JWT Secret key is not configured");
            }

            if (string.IsNullOrEmpty(issuer))
            {
                _logger.LogWarning("JWT Issuer is not configured");
            }

            if (string.IsNullOrEmpty(audience))
            {
                _logger.LogWarning("JWT Audience is not configured");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiryDate = DateTime.UtcNow.AddDays(1);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expiryDate,
                signingCredentials: creds
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            _logger.LogInformation("JWT token generated successfully for user with ID: {UserId}, Expires: {ExpiryDate}",
                user.Id, expiryDate);

            return (jwtToken, expiryDate);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while generating JWT token for user with ID: {UserId}", user.Id);
            throw;
        }
    }

    public string GenerateRefreshToken()
    {
        _logger.LogInformation("Generating refresh token");

        try
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            var refreshToken = Convert.ToBase64String(randomNumber);

            _logger.LogInformation("Refresh token generated successfully");

            return refreshToken;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while generating refresh token");
            throw;
        }
    }

    public async Task<(string refreshToken, DateTime expiresAtUtc)>  GenerateAndSaveRefreshTokenAsync(User user)
    {
        _logger.LogInformation("Generating and saving refresh token for user with ID: {UserId}", user.Id);

        try
        {
            var refreshToken = GenerateRefreshToken();
            var expiryDate = DateTime.UtcNow.AddDays(7);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiresAtUtc = expiryDate;

            _logger.LogInformation("Saving refresh token to database for user with ID: {UserId}, Expires: {ExpiryDate}",
                user.Id, expiryDate);

            await unitOfWork.CompleteAsync();

            _logger.LogInformation("Refresh token saved successfully for user with ID: {UserId}", user.Id);

            return (refreshToken,expiryDate);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while generating and saving refresh token for user with ID: {UserId}",
                user.Id);
            throw;
        }
    }




    public void WriteAuthTokenAsHttpOnlyCookie(string cookieName, string token,
        DateTime expiration)
    {
        _httpContextAccessor!.HttpContext.Response.Cookies.Append(cookieName,
            token, new CookieOptions
            {
                HttpOnly = true,
                Expires = expiration,
                IsEssential = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
            });
    }
}
