using LoginAPI.Models;
using LoginAPI.Models.Dtos;
using LoginAPI.Models.Dtos.Account;
namespace LoginAPI.Services.AccountService;

public interface IAccountService
{
    Task<ServiceResponse<User>> ValidateUser(string Identifier);
    Task<ServiceResponse<User>> RegisterUser(User user);
    Task<ServiceResponse<LoginDto>> Login(LoginDto user);
    Task<User> ChangePassword(User user);
    Task<ServiceResponse<bool>> Logout(LogoutDto user);
    Task<ServiceResponse<User>> GetUserInfo(Guid id);
}
