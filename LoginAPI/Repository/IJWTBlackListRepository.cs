using LoginAPI.Models;
using LoginAPI.Repository;

namespace LoginAPI.Repository;

public interface IJWTBlackListRepository : IRepository<JWTBlackList>
{
    Task<string?> GetByToken(string token);
}
