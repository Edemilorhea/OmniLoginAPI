namespace LoginAPI.Services.AuthenticationService;

public interface IJWTService
{
    public string GenerateToken(string userId);
}
