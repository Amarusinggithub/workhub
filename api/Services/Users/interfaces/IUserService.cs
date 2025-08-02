using api.DTOs.Users;
using api.Models;

namespace api.Services.Users.interfaces;

public interface IUserService
{

    public Task<User> GetUserById(Guid id);

    Task<User?> GetByEmail(string email);

    public Task<UserDto?> AddUser(string lastName,string firstName,string password, string email);



    Task<UserDto?> Authenticate(string email, string password);
    Task<IEnumerable<UserDto>> GetAll();
    Task<User?> GetById(Guid id);

    Task<UserDto?> AddAndUpdateUser(User? userObj);

    public Task RefreshTokenAsync(string refreshToken, Guid userId);
    public Task<bool> ValidateRefreshTokenAsync(string refreshToken, Guid userId);
    public int? GetUserIdFromToken(string token);
    public Task<bool> RevokeRefreshTokenAsync(Guid userId);

}
