using api.DTOs.Users;
using api.Models;

namespace api.Services.Users.interfaces;

public interface IUserService
{


    Task<User?> GetByEmail(string email);

    public Task<bool> AddUser(string lastName,string firstName,string password, string email);



    Task<IEnumerable<UserDto>> GetAll();
    Task<User?> GetById(Guid id);

    Task<UserDto?> AddAndUpdateUser(User? userObj);



}
