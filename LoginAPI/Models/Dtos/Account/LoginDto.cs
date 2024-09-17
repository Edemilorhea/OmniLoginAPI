using System.ComponentModel.DataAnnotations;

namespace LoginAPI.Models.Dtos.Account;

public class LoginDto
{
    [Required]
    public required string Identifier  { get; set; } // Username or Email
    [Required]
    public required string Password { get; set; }
    public string? JwtToken { get; set; }
}

