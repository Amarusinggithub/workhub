using api.Data.interfaces;
using api.DTOs.Users;
using api.Models;
using api.Services.Auth.interfaces;
using api.Services.Users;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace api.Services.Auth;

public class AuthService(
    ILogger<UserService> logger,
    UserManager<User> userManager,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IPasswordHashService hashService,
    ITokenService tokenService)
    : IAuthService
{
    private readonly ILogger<UserService> _logger = logger;
    private readonly UserManager<User> _userManager = userManager;
    private readonly IMapper _mapper = mapper;

    public async Task<UserDto?> Authenticate(string email, string password)
    {
        _logger.LogInformation("Starting authentication process");

        try
        {
            var user = await GetByEmail(email);

            if (user == null)
            {
                _logger.LogWarning("Authentication failed - user not found");
                return null;
            }

            if (user.PasswordHash != null && hashService.Verify(password, user.PasswordHash))
            {
                _logger.LogInformation("Authentication successful for user: {UserId}", user.Id);

                user.LastLoggedIn = DateTime.UtcNow;
                await unitOfWork.CompleteAsync();




                    var (jwtToken, expirationDateInUtc) = await tokenService.GenerateToken(user);
                    var (refreshTokenValue, refreshTokenExpirationDateInUtc) = await tokenService.GenerateAndSaveRefreshTokenAsync(user);

                    //var refreshTokenExpirationDateInUtc = DateTime.UtcNow.AddDays(7);

                    user.RefreshToken = refreshTokenValue;
                    user.RefreshTokenExpiresAtUtc = refreshTokenExpirationDateInUtc;

                    await _userManager.UpdateAsync(user);

                    tokenService.WriteAuthTokenAsHttpOnlyCookie("ACCESS_TOKEN", jwtToken, expirationDateInUtc);
                    tokenService.WriteAuthTokenAsHttpOnlyCookie("REFRESH_TOKEN", user.RefreshToken, refreshTokenExpirationDateInUtc);


                return new UserDto
                {
                    id=user.Id,
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
            else
            {
                _logger.LogWarning("Authentication failed - invalid credentials for user: {UserId}", user.Id);
                return null;
            }
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
            var existingUser = await GetByEmail(email);
            if (existingUser != null)
            {
                _logger.LogWarning("User creation failed - email already exists");
                return null;
            }

            User entity = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PasswordHash = hashService.Hash(password),
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            await unitOfWork.Users.Add(entity);
            await unitOfWork.CompleteAsync();

            _logger.LogInformation("User created successfully with ID: {UserId}", entity.Id);

            return await Authenticate(entity.Email, password);
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
            User? userExist = await unitOfWork.Users.GetByEmail(email);

            if (userExist != null)
            {
                _logger.LogInformation("User found - UserId: {UserId}", userExist.Id);
            }
            else
            {
                _logger.LogInformation("User not found by email");
            }

            return userExist;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving user by email");
            throw;
        }
    }




    public async Task<bool> ValidateToken( string token ,Guid userId)
    {
        return await tokenService.ValidateRefreshTokenAsync(token,userId);
    }

    public async Task<bool> Logout(Guid userId)
    {
        return await tokenService.RevokeRefreshTokenAsync(userId);
    }

}


