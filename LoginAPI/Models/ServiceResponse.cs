namespace LoginAPI.Models;

public class ServiceResponse<T>
{
    public required int StatusCode { get; set; }
    public required T? Data { get; set; }
    public required string Message { get; set; }
}
