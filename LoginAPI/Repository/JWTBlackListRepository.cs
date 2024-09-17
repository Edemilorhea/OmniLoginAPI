using LoginAPI.Database;
using LoginAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginAPI.Repository;

public class JWTBlackListRepository : IJWTBlackListRepository
{
    private readonly LoginContext _context;
    private readonly ILogger<JWTBlackListRepository> _logger;

    public JWTBlackListRepository(LoginContext context, ILogger<JWTBlackListRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task AddAsync(JWTBlackList entity)
    {
        await _context.JWTBlackList.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _context.JWTBlackList.FindAsync(id);
        if(entity != null)
        {
            _context.JWTBlackList.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<JWTBlackList>> GetAllAsync()
    {
        return await _context.JWTBlackList.ToListAsync();
    }

    public async Task<JWTBlackList?> GetByIdAsync(Guid id)
    {
        return await _context.JWTBlackList.FindAsync(id);
    }

    public async Task<string?> GetByToken(string token)
    {
        var result = await _context.JWTBlackList.AsNoTracking().FirstOrDefaultAsync(x => x.Token == token);

        return result?.Token;
    }

    public Task UpdateAsync(JWTBlackList entity)
    {
        throw new NotImplementedException();
    }

}
