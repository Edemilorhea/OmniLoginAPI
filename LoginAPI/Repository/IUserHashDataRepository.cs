using LoginAPI.Migrations;
using LoginAPI.Models;
using LoginAPI.Repository;

namespace LoginAPI.Repository;

public interface IUserHashDataRepository : IRepository<UserHashData>
{

    public Task<UserHashData?> GetByUserId(Guid userId);
}
