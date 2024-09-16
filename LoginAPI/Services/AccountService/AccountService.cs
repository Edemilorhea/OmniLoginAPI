using BCrypt.Net;
using LoginAPI.Models;
using LoginAPI.Repository;

namespace LoginAPI.Services.AccountService;

public class AccountService : IAccountService
{
    private readonly IUserRepository _userRepository;
    private readonly IRepository<UserHashData> _userHashDataRepository;

    public AccountService(
        IUserRepository userRepository,
        IRepository<UserHashData> userHashDataRepository
    )
    {
        _userRepository = userRepository;
        _userHashDataRepository = userHashDataRepository;
    }

    public Task<User> ChangePassword(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<User>> GetUserInfo(Guid id)
    {
        var result = await _userRepository.GetByIdAsync(id);
        return new ServiceResponse<User>
        {
            StatusCode = 200,
            Data = result,
            Message = "User found"
        };

    }

    public Task<User> Login(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User> Logout(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<User>> RegisterUser(User user)
    {
        string userSalt = BCrypt.Net.BCrypt.GenerateSalt(10);
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, userSalt);

        await _userRepository.AddAsync(user);
        await _userHashDataRepository.AddAsync(

            new UserHashData { UserId = user.UserId, Salt = userSalt }
        );

        return new ServiceResponse<User>
        {
            StatusCode = 200,
            Data = user,
            Message = "User created"
        };
    }

    public async Task<ServiceResponse<User>> ValidateUser(User user)
    {
        if (user == null)
        {
            throw new Exception("User not found");
        }
        var result = await _userRepository.GetUserByEmail(user.Email);
        if (result != null)
        {
            return new ServiceResponse<User>
            {
                StatusCode = 200,
                Data = result,
                Message = "User found"
            };
        }
        else
        {
            return new ServiceResponse<User>
            {
                StatusCode = 200,
                Data = null,
                Message = "User not found"
            };
        }
    }
}
