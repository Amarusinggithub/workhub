using api.Models;
using api.Repository.interfaces;

namespace api.Repository.Users.interfaces;

public interface IUserRepository: IGenericRepository<User,Guid>
{
    Task<User?> AddAndUpdateUser(User? userObj);


    Task<User?> GetByEmail(string email);

    public Task<User?> GetUserByRefreshTokenAsync(string refreshToken);



}
