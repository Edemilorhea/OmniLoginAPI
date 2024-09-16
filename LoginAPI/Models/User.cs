using System.ComponentModel.DataAnnotations;

namespace LoginAPI.Models;

public partial class User
{
    public Guid UserId { get; set; }
    [Required]
    public required string Email { get; set; }
    [Required]
    public required string UserName { get; set; }
    [Required]
    public required string Password { get; set; }
}
