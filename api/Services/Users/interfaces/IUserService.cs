using api.Models;

namespace api.Services.interfaces;

public interface IUserService
{
    public Task<bool> AddUser(string lastName,string firstName,string password, string email);

    public Task<User> GetUserById(int id);

    public bool Authenticate(string password, string email);

    Task<User?> GetByEmail(string email);


}
