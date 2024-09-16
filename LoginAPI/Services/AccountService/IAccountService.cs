using LoginAPI.Models;
namespace LoginAPI.Services.AccountService;

public interface IAccountService
{
    Task<ServiceResponse<User>> ValidateUser(User user);
    Task<ServiceResponse<User>> RegisterUser(User user);
    Task<User> Login(User user);
    Task<User> ChangePassword(User user);
    Task<User> Logout(User user);
    Task<ServiceResponse<User>> GetUserInfo(Guid id);
}
