namespace LoginAPI.Models.Dtos.Account;

public class ChangePasswordDto
{
    public required string OldPassword { get; set; }
    public required string NewPassword { get; set; }
    public string? JWTToken { get; set; }
}
