using LoginAPI.Database;
using LoginAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginAPI.Repositroy;

public class UserHashDataRepository : IRepository<UserHashData>
{
    private readonly LoginContext _context;

    private readonly ILogger<UserHashDataRepository> _logger;

    public UserHashDataRepository(LoginContext context, ILogger<UserHashDataRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task AddAsync(UserHashData entity)
    {
        await _context.UserHashData.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string id)
    {
        var userHashData = await _context.UserHashData.FindAsync(id);
        if (userHashData != null)
        {
            _context.UserHashData.Remove(userHashData);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<UserHashData>> GetAllAsync()
    {
        return await _context.UserHashData.ToListAsync();
    }

    public async Task<UserHashData> GetByIdAsync(string id)
    {
        var result = await _context.UserHashData.FindAsync(id);
        if (result != null)
        {
            return result;
        }
        else
        {
            _logger.LogWarning($"UserHashData {id} not found");
            throw new Exception("UserHashData not found");
        }
    }

    public Task UpdateAsync(UserHashData entity)
    {
        _context.UserHashData.Update(entity);
        return _context.SaveChangesAsync();
    }
}
