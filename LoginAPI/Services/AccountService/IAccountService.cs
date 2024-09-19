using LoginAPI.Models;
using LoginAPI.Models.Dtos;
using LoginAPI.Models.Dtos.Account;
namespace LoginAPI.Services.AccountService;

public interface IAccountService
{
    Task<ServiceResponse<User>> ValidateUser(string Identifier);
    Task<ServiceResponse<User>> RegisterUser(User user);
    Task<ServiceResponse<LoginDto>> Login(LoginDto user);
    Task<ServiceResponse<bool>> ChangePassword(ChangePasswordDto requestData, string userId);
    Task<ServiceResponse<bool>> Logout(string user);
    Task<ServiceResponse<User>> GetUserInfo(Guid id);
}
