using api.Models;
using api.Repository.interfaces;


namespace api.Repositorys.interfaces;

public interface IUserRepository: IGenericRepository<User>
{

    public Task<User> Authenticate(string password, string email);

    Task<User?> GetByEmail(string email);




}
