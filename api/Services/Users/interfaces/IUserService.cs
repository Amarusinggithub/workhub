using api.DTOs.Users;
using api.Models;

namespace api.Services.Users.interfaces;

public interface IUserService
{

    public Task<User> GetUserById(int id);

    Task<User?> GetByEmail(string email);

    public Task<UserDto?> AddUser(string lastName,string firstName,string password, string email);



    Task<UserDto?> Authenticate(string email, string password);
    Task<IEnumerable<UserDto>> GetAll();
    Task<User?> GetById(int id);

    Task<UserDto?> AddAndUpdateUser(User? userObj);

    public Task RefreshTokenAsync(string refreshToken, int userId);
    public Task<bool> ValidateRefreshTokenAsync(string refreshToken, int userId);
    public int? GetUserIdFromToken(string token);
    public Task<bool> RevokeRefreshTokenAsync(int userId);

}
