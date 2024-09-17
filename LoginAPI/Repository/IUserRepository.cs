using LoginAPI.Models;
using LoginAPI.Repository;

namespace LoginAPI.Repository;

public interface IUserRepository : IRepository<User>
{
    public Task<User?> GetUserByEmailOrUserName(string email);
}
