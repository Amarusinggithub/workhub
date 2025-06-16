using api.Models;

namespace api.Services.interfaces;

public interface IUserService
{
    public Task<bool> AddUser(User entity);

    public Task<User> GetUserById(int id);
}
