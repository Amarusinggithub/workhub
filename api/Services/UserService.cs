using api.Models;
using api.Repository.interfaces;
using api.Services.interfaces;

namespace api.Services;

public class UserService: IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(ILogger<UserService> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> AddUser(User entity)
    {
        await _unitOfWork.Users.Add(entity);
        await _unitOfWork.CompleteAsync();
        return true;

    }

    public async Task<User> GetUserById(int id)
    {
        return await _unitOfWork.Users.GetById(id);
    }



    public async Task<User> Authenticate(string password, string email)
    {
        return await _unitOfWork.Users.Authenticate(password, email);

    }


}
