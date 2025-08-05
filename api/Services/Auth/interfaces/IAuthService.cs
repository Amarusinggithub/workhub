using api.DTOs.Users;
using api.Models;

namespace api.Services.Auth.interfaces;

public interface IAuthService
{
    Task<UserDto?> Authenticate(string email, string password);
    Task<User?> GetByEmail(string email);

    public Task<UserDto?> Register(string lastName,string firstName,string password, string email);


    public Task<bool> Logout(Guid userId);

    public Task<UserDto?> GetById(Guid id);

}
