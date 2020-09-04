using System.Threading;
using System.Threading.Tasks;
using Ackee.DataAccess.EfCore.Mapping;
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

        public DbSet<Events.EventData> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventDataMapping).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            await UncommittedEventHandler.Handel(this);

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}