using api.Data.interfaces;
using api.DTOs.Auth;
using api.DTOs.Users.Responses;
using api.Models;
using api.Services.Auth.interfaces;
using api.Services.Users.interfaces;

namespace api.Services.Users;

public class UserService(ILogger<UserService> logger, IUnitOfWork unitOfWork, IPasswordHashService hashService, ITokenService tokenService)
    : IUserService
{
    private readonly ILogger<UserService> _logger = logger;

    public async Task<UserResponseDto?> Authenticate(string email, string password)
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

                AuthTokenResponse authTokens = new AuthTokenResponse
                {
                    AccessToken = await tokenService.GenerateToken(user),
                    RefreshToken = await tokenService.GenerateAndSaveRefreshTokenAsync(user),
                };

                return new UserResponseDto
                {
                    email = user.Email,
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    headerImage = user.HeaderImage,
                    jobTItle = user.JobTItle,
                    organization = user.Organization,
                    isActive = user.IsActive,
                    location = user.Location,
                    profilePicture = user.ProfilePicture,
                    lastLoggedIn = user.LastLoggedIn,
                    createdAt = user.CreatedAt,
                    AuthTokens = authTokens
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

    public async Task<User> GetUserById(int id)
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

    public async Task<UserResponseDto?> AddUser(string lastName, string firstName, string password, string email)
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

    public async Task<User?> AddAndUpdateUser(User? userObj)
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

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while adding/updating user with ID: {UserId}", userObj.Id);
            throw;
        }
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        _logger.LogInformation("Retrieving all users");

        try
        {
            var users = await unitOfWork.Users.GetAll();
            var userCount = users.Count();

            _logger.LogInformation("Retrieved {UserCount} users", userCount);

            return users;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving all users");
            throw;
        }
    }

    public async Task<User?> GetById(int id)
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
}
