namespace LoginAPI.Models;

public partial class UserHashData
{
    public Guid UserHashDataId { get; set; }
    public required string UserId { get; set; }
    public required string Salt { get; set; }
}
