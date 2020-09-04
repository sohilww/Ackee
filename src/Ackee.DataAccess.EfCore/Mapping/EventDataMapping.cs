using Ackee.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ackee.DataAccess.EfCore.Mapping
{
    public class EventDataMapping : IEntityTypeConfiguration<EventData>
    {
        public void Configure(EntityTypeBuilder<EventData> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Ignore(a => a.DomainEvent);
        }
    }
}