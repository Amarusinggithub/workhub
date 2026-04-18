using api.DTOs.Users;
using api.Models;
using api.Services.Auth.interfaces;
using api.Services.Users;
using Microsoft.AspNetCore.Identity;

namespace api.Services.Auth;

public class AuthService(
    ILogger<UserService> logger,
    UserManager<User> userManager,
    ITokenService tokenService,
    IHttpContextAccessor httpContextAccessor)
    : IAuthService
{
    private readonly ILogger<UserService> _logger = logger;
    private readonly UserManager<User> _userManager = userManager;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<UserDto?> Authenticate(string email, string password)
    {
        _logger.LogInformation("Starting authentication process");

        try
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.LogWarning("Authentication failed - user not found");
                return null;
            }

            if (!await _userManager.CheckPasswordAsync(user, password))
            {
                _logger.LogWarning("Authentication failed - invalid credentials for user: {UserId}", user.Id);
                return null;
            }

            _logger.LogInformation("Authentication successful for user: {UserId}", user.Id);

            var (jwtToken, expirationDateInUtc) = await _tokenService.GenerateToken(user);
            var (refreshTokenValue, refreshTokenExpirationDateInUtc) = await _tokenService.GenerateAndSaveRefreshTokenAsync(user);
            var remoteIpAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress;

            user.SetRefreshToken(refreshTokenValue, refreshTokenExpirationDateInUtc);
            user.RecordLogin(ipAddress: remoteIpAddress?.ToString());

            await _userManager.UpdateAsync(user);

            _tokenService.WriteAuthTokenAsHttpOnlyCookie("ACCESS_TOKEN", jwtToken, expirationDateInUtc);
            _tokenService.WriteAuthTokenAsHttpOnlyCookie("REFRESH_TOKEN", user.RefreshToken, refreshTokenExpirationDateInUtc);

            return MapToDto(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during authentication process");
            throw;
        }
    }

    public async Task<UserDto?> Register(string lastName, string firstName, string password, string email)
    {
        _logger.LogInformation("Creating new user account");

        try
        {
            var entity = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserName = email,
                CreatedAt = DateTime.UtcNow,
                IsActive = true,
            };

            var result = await _userManager.CreateAsync(entity, password);
            if (!result.Succeeded)
            {
                _logger.LogWarning("User creation failed: {Errors}",
                    string.Join(", ", result.Errors.Select(e => e.Description)));
                return null;
            }

            _logger.LogInformation("User created successfully with ID: {UserId}", entity.Id);
            return await Authenticate(entity.Email!, password);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating user");
            throw;
        }
    }

    public async Task<User?> GetByEmail(string email)
    {
        _logger.LogInformation("Retrieving user by email");

        try
        {
            return await _userManager.FindByEmailAsync(email);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving user by email");
            throw;
        }
    }

    public async Task<UserDto?> GetById(Guid id)
    {
        _logger.LogInformation("Retrieving user by ID: {UserId}", id);

        try
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return null;
            return MapToDto(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving user by ID");
            throw;
        }
    }

    public async Task<bool> Logout(Guid userId)
    {
        return await _tokenService.RevokeRefreshTokenAsync(userId);
    }

    private static UserDto MapToDto(User user) => new()
    {
        id = user.Id,
        email = user.Email,
        firstName = user.FirstName,
        lastName = user.LastName,
        headerImageUrl = user.HeaderImageUrl,
        jobTitle = user.JobTitle,
        organization = user.Organization,
        isActive = user.IsActive,
        location = user.Location,
        avatarUrl = user.AvatarUrl,
        lastLoggedIn = user.LastLoggedIn,
        createdAt = user.CreatedAt,
    };
}
