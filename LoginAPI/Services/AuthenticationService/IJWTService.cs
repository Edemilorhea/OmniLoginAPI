using LoginAPI.Models;
using LoginAPI.Models.Dtos.Account;

namespace LoginAPI.Services.AuthenticationService;

public interface IJWTService
{
    public string GenerateToken(string userId);

    public Task<ServiceResponse<bool>> AddToken2BlackList(LogoutDto data);

    public Task<ServiceResponse<bool>> CheckTokenInBlackList(string token);
}
