using System.IdentityModel.Tokens.Jwt;
using api.Data.interfaces;
using api.DTOs.Users;
using api.Exceptions;
using api.Models;
using api.Services.Auth.interfaces;
using api.Services.Users.interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace api.Services.Users;

public class UserService(ILogger<UserService> logger, UserManager<User> userManager,  IMapper mapper,IUnitOfWork unitOfWork, IPasswordHashService hashService, ITokenService tokenService)
    : IUserService
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

    public async Task<User> GetUserById(Guid id)
    {
        _logger.LogInformation("Retrieving user by ID: {UserId}", id);

        try
        {
            var user = await unitOfWork.Users.GetById(id);

            _logger.LogInformation("User retrieved successfully: {UserId}", id);

            return user;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving user by ID: {UserId}", id);
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

    public async Task<UserDto?> AddUser(string lastName, string firstName, string password, string email)
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

    public async Task<UserDto?> AddAndUpdateUser(User? userObj)
    {
        if (userObj == null)
        {
            _logger.LogWarning("Attempted to add/update null user object");
            return null;
        }

        _logger.LogInformation("Adding/updating user with ID: {UserId}", userObj.Id);

        try
        {
            var result = await unitOfWork.Users.AddAndUpdateUser(userObj);

            if (result != null)
            {
                _logger.LogInformation("User add/update successful for ID: {UserId}", result.Id);
            }
            else
            {
                _logger.LogWarning("User add/update returned null for user with ID: {UserId}", userObj.Id);
            }



            return new UserDto
            {
                id=result!.Id,
                email = result.Email,
                firstName = result.FirstName,
                lastName = result.LastName,
                headerImageUrl = result.HeaderImageUrl,
                jobTitle = result.JobTitle,
                organization = result.Organization,
                isActive = result.IsActive,
                location = result.Location,
                avatarUrl = result.AvatarUrl,
                lastLoggedIn = result.LastLoggedIn,
                createdAt = result.CreatedAt,
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while adding/updating user with ID: {UserId}", userObj.Id);
            throw;
        }
    }

    public async Task<IEnumerable<UserDto>> GetAll()
    {
        _logger.LogInformation("Retrieving all users");

        try
        {


            var users = await unitOfWork.Users.GetAll();
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);

            var userCount = users.Count();

            _logger.LogInformation("Retrieved {UserCount} users", userCount);

            return userDtos;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving all users");
            throw;
        }
    }

    public async Task<User?> GetById(Guid id)
    {
        _logger.LogInformation("Retrieving user by ID: {UserId}", id);

        try
        {
            var user = await unitOfWork.Users.GetById(id);

            _logger.LogInformation("User retrieved successfully: {UserId}", id);

            return user;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving user by ID: {UserId}", id);
            throw;
        }
    }


     public async Task<bool> RevokeRefreshTokenAsync(Guid userId)
    {
        _logger.LogInformation("Revoking refresh token for user with ID: {UserId}", userId);

        try
        {
            var user = await unitOfWork.Users.GetById(userId);

            user.RefreshToken = null;
            user.RefreshTokenExpiresAtUtc = null;
            await unitOfWork.CompleteAsync();

            _logger.LogInformation("Refresh token revoked successfully for user: {UserId}", userId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while revoking refresh token for user: {UserId}", userId);
            return false;
        }
    }


    public int? GetUserIdFromToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jsonToken = tokenHandler.ReadJwtToken(token);

            var userIdClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == "userId");
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }

            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while extracting user ID from token");
            return null;
        }
    }


    public async Task<bool> ValidateRefreshTokenAsync(string refreshToken, Guid userId)
    {
        _logger.LogInformation("Validating refresh token for user with ID: {UserId}", userId);

        try
        {
            var user = await unitOfWork.Users.GetById(userId);

            if (user.RefreshToken != refreshToken)
            {
                _logger.LogWarning("Refresh token validation failed - token mismatch for user: {UserId}", userId);
                return false;
            }

            if (user.RefreshTokenExpiresAtUtc <= DateTime.UtcNow)
            {
                _logger.LogWarning("Refresh token validation failed - token expired for user: {UserId}", userId);
                return false;
            }

            _logger.LogInformation("Refresh token validated successfully for user: {UserId}", userId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while validating refresh token for user: {UserId}", userId);
            return false;
        }
    }

    public async Task RefreshTokenAsync(string? refreshToken, Guid userId)
    {
        _logger.LogInformation("Refreshing access token for user with ID: {UserId}", userId);

        try
        {
            if(string.IsNullOrEmpty(refreshToken))
            {
                throw new RefreshTokenException("Refresh token is missing.");
            }

            if (!await ValidateRefreshTokenAsync(refreshToken, userId))
            {
                _logger.LogWarning("Token refresh failed - invalid refresh token for user: {UserId}", userId);
            }
           // var user = await unitOfWork.Users.GetUserByRefreshTokenAsync(refreshToken);
            var user = await unitOfWork.Users.GetById(userId);

            if (user == null)
            {
                throw new RefreshTokenException("Unable to retrieve user for refresh token");
            }
            var (newAccessToken, expirationDateInUtc) = await tokenService.GenerateToken(user);
            var (newRefreshToken, refreshTokenExpirationDateInUtc) = await tokenService.GenerateAndSaveRefreshTokenAsync(user);


            await _userManager.UpdateAsync(user);

            tokenService.WriteAuthTokenAsHttpOnlyCookie("ACCESS_TOKEN", newAccessToken, expirationDateInUtc);
             tokenService.WriteAuthTokenAsHttpOnlyCookie("REFRESH_TOKEN", newRefreshToken, refreshTokenExpirationDateInUtc);

            _logger.LogInformation("Tokens refreshed successfully for user: {UserId}", userId);


        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while refreshing tokens for user: {UserId}", userId);
            throw;
        }

    }
}
