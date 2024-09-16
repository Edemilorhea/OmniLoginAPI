namespace LoginAPI.Models;

public partial class UserHashData
{
    public Guid UserHashDataId { get; set; }
    public required Guid UserId { get; set; }
    public required string Salt { get; set; }
}
