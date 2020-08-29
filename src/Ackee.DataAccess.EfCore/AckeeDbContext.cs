using Microsoft.EntityFrameworkCore;

namespace Ackee.DataAccess.EfCore
{
    public abstract class AckeeDbContext : DbContext, IAckeeSession
    {
        protected AckeeDbContext()
        {
            
        }
        protected AckeeDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}