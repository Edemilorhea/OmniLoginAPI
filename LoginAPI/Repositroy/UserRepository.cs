using LoginAPI.Database;
using LoginAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LoginAPI.Repositroy;

public class UserRepository : IRepository<User>
{
    private readonly LoginContext _context;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(LoginContext context, ILogger<UserRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task AddAsync(User entity)
    {
        await _context.Users.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string id)
    {
        var user =await _context.Users.FindAsync(id);
        if(user != null){
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> GetByIdAsync(string id)
    {
        var result = await _context.Users.FindAsync(id);
        if (result != null)
        {
            return result;
        }else
        {
            _logger.LogWarning($"User {id} not found");
            throw new Exception("User not found");
        }
    }

    public async Task UpdateAsync(User entity)
    {
        _context.Users.Update(entity);
        await _context.SaveChangesAsync();
    }

}
