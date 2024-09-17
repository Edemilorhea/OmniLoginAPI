namespace LoginAPI.Models;

public class JWTBlackList
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public required string Token { get; set; }

    public required DateTime ExpireTime { get; set; }

    public required DateTime CreateTime { get; set; }
}
