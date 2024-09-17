using LoginAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginAPI.Database
{
    public class LoginContext : DbContext
    {
        public LoginContext(DbContextOptions<LoginContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserHashData> UserHashData { get; set; }
        public DbSet<JWTBlackList> JWTBlackList { get; set; }
    }
    
    
}
