using LoginAPI.Models;
namespace LoginAPI.Services.AccountService;

public interface IAccountService
{
    Task<ServiceResponse<User>> ValidateUser(User user);
    Task<ServiceResponse<User>> RegisterUser(User user);
    Task<User> LoginUser(User user);
    Task<User> ChangePassword(User user);
    Task<User> LogoutUser(User user);
}
