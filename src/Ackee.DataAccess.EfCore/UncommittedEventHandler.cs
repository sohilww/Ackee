using System.Linq;
using System.Threading.Tasks;
using Ackee.Domain.Model;
using Ackee.Events;

namespace Ackee.DataAccess.EfCore
{
    public class UncommittedEventHandler
    {
        public static async Task Handel(AckeeDbContext context)
        {
            var aggregateRoots = context.ChangeTracker.Entries<IUncommittedEvents>()
                .Select(a => a.Entity).ToList();


            var uncommittedEvents = aggregateRoots
                .SelectMany(a => EventDataFactory.Create(a.UncommittedEvent));

            context.Events.AddRange(uncommittedEvents);

            aggregateRoots.ForEach(a => a.UncommittedEvent.Clear());
        }
    }
}