using api.Models;
using api.Repository.interfaces;


namespace api.Repositories.interfaces;

public interface IUserRepository: IGenericRepository<User>
{
    Task<User?> AddAndUpdateUser(User? userObj);


    Task<User?> GetByEmail(string email);




}
