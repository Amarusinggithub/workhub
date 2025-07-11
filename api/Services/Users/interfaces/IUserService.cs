using api.DTOs.Auth;
using api.DTOs.Users.Responses;
using api.Models;

namespace api.Services.Users.interfaces;

public interface IUserService
{

    public Task<User> GetUserById(int id);

    Task<User?> GetByEmail(string email);

    public Task<UserResponseDto?> AddUser(string lastName,string firstName,string password, string email);



    Task<UserResponseDto?> Authenticate(string email, string password);
    Task<IEnumerable<User>> GetAll();
    Task<User?> GetById(int id);

    Task<User?> AddAndUpdateUser(User? userObj);

}
