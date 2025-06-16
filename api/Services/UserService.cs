using api.Models;
using api.Repository.interfaces;
using api.Services.interfaces;

namespace api.Services;

public class UserService: IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidtateDictionary _validationDictionary;

    public UserService(ILogger<UserService> logger, IUnitOfWork unitOfWork,IValidtateDictionary validationDictionary)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validationDictionary = validationDictionary;
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
}
