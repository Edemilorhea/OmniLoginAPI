using LoginAPI.Database;
using LoginAPI.Models;
using LoginAPI.Repositroy;
using Microsoft.EntityFrameworkCore;

namespace LoginAPI.Repository;

public class UserHashDataRepository : IUserHashDataRepository
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

    public async Task DeleteAsync(Guid id)
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

    public async Task<UserHashData?> GetByIdAsync(Guid id)
    {
        var result = await _context.UserHashData.FindAsync(id);
        if (result != null)
        {
            return result;
        }
        else
        {
            _logger.LogWarning($"UserHashData {id} not found");
            return null;
        }
    }

    public async Task<UserHashData?> GetByUserId(Guid userId)
    {
        var result = await _context.UserHashData.FirstOrDefaultAsync(x => x.UserId == userId);
        if(result != null)
        {
            return result;
        }
        else
        {
            _logger.LogWarning($"UserHashData {userId} not found");
            return null;
        }
    }

    public Task UpdateAsync(UserHashData entity)
    {
        _context.UserHashData.Update(entity);
        return _context.SaveChangesAsync();
    }
}
