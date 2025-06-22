using api.Models;
using api.Repository.interfaces;
using api.Services.interfaces;

namespace api.Services;

public class UserService: IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHashService _hashService;

    public UserService(ILogger<UserService> logger, IUnitOfWork unitOfWork,IPasswordHashService hashService)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _hashService = hashService;
    }

    public async Task<bool> AddUser(string lastName,string firstName,string password, string email)
    {

        User entity=new User
        {
            FirtName = firstName,
            LastName = lastName,
            Email = email,
            PasswordHash = password
        };

        await _unitOfWork.Users.Add(entity);
        await _unitOfWork.CompleteAsync();
        return true;

    }

    public async Task<User> GetUserById(int id)
    {
        return await _unitOfWork.Users.GetById(id);
    }



    public bool Authenticate(string password, string passwordHash)
    {


       return  _hashService.Verify(password, passwordHash);

    }

    public  async Task<User?> GetByEmail(string email)
    {
        User? userExist = await _unitOfWork.Users.GetByEmail(email);
        return userExist;    }

    public async Task<User> Register(string lastName, string firstName, string password, string email)
    {
        User user = new User
        {
            FirtName = firstName,
            LastName = lastName,
            Email = email,
            PasswordHash = _hashService.Hash(password)
        };

        await _unitOfWork.Users.Add(user);

        return user;

    }
}
