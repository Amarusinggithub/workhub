using backend.Models;

namespace backend.Services.interfaces;

public interface IUserService
{
    public Task<bool> AddUser(User entity);

    public Task<User> GetUserById(int id);
}
