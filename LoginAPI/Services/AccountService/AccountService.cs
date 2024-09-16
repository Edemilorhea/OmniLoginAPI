using BCrypt.Net;
using LoginAPI.Models;
using LoginAPI.Models.Dtos.Account;
using LoginAPI.Repository;
using LoginAPI.Repositroy;

namespace LoginAPI.Services.AccountService;

public class AccountService : IAccountService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserHashDataRepository _userHashDataRepository;

    public AccountService(
        IUserRepository userRepository,
        IUserHashDataRepository userHashDataRepository
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

    public async Task<ServiceResponse<User>> Login(LoginDto user)
    {
        var dBUserData = await _userRepository.GetUserByEmailOrUserName(user.Identifier);
        
        if(dBUserData == null)
        {
            return new ServiceResponse<User>
            {
                StatusCode = 404,
                Data = null,
                Message = "User not found"
            };
        }

        var result = BCrypt.Net.BCrypt.Verify(
            user.Password,
            dBUserData.Password);

        return new ServiceResponse<User>
        {
            StatusCode = 200,
            Data = result ? dBUserData : null,
            Message = result ? "Login Sussess" : "Login failed"
        };
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

        // TODO: 接下來新增JWT Token 


        return new ServiceResponse<User>
        {
            StatusCode = 200,
            Data = user,
            Message = "User created"
        };
    }

    public async Task<ServiceResponse<User>> ValidateUser(string Identifier)
    {
        var result = await _userRepository.GetUserByEmailOrUserName(Identifier);
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
