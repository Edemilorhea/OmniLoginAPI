namespace LoginAPI.Models.Dtos.Account;

public class LogoutDto
{
    public required string Identifier { get; set; }
    public string? JwtToken { get; set; }
}
