using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sportradar.Core.Entities;

namespace Sportradar.Infrastructure.EntityConfig;

public class EventConfig : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasDiscriminator<EventType>("EventType")
            .HasValue<OneOnOneEvent>(EventType.OneOnOne)
            .HasValue<TeamEvent>(EventType.Team)
            .HasValue<FreeForAllEvent>(EventType.FreeForAll);

        builder.HasOne(e => e.Result)
            .WithOne(r => r.Event)
            .HasForeignKey<Result>(r => r.EventId)
            .IsRequired(false);

        builder.HasOne(e => e.Competition)
            .WithMany(c => c.Events)
            .HasForeignKey(e => e.CompetitionId)
            .IsRequired(false);

        builder.HasIndex(e => e.CompetitionId);
        builder.HasIndex(e => e.LocationId);
        builder.HasIndex(e => e.Status);
    }
}
