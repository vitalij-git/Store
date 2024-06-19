using Microsoft.EntityFrameworkCore;

namespace Store.Database
{
    public class DatabaseDbContext : DbContext
    {
        public DatabaseDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
