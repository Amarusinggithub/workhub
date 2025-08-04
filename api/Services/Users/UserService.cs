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




    public async Task<User> GetById(Guid id)
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

    public async Task<bool> AddUser(string lastName, string firstName, string password, string email)
    {
        _logger.LogInformation("Creating new user account");

        try
        {
            var existingUser = await GetByEmail(email);
            if (existingUser != null)
            {
                _logger.LogWarning("User creation failed - email already exists");
                return false;
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

           bool isUserAdded= await unitOfWork.Users.Add(entity);
            await unitOfWork.CompleteAsync();

            _logger.LogInformation("User created successfully with ID: {UserId}", entity.Id);

            return isUserAdded;
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






    }

