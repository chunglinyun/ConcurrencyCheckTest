using Microsoft.EntityFrameworkCore;

namespace ConcurrencyCheckTest.Entity
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
        public DbSet<Wager> Wagers { get; set; }
        // Define your DbSets and DbEntities here
    }
}
