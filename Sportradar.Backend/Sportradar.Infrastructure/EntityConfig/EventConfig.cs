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
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Competition)
            .WithMany(c => c.Events)
            .HasForeignKey(e => e.CompetitionId)
            .IsRequired(false);

        builder.HasOne(c => c.Sport)
            .WithMany()
            .HasForeignKey(c => c.SportId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(e => e.SportId)
            .HasColumnName("_SportId");

        builder.Property(e => e.CompetitionId)
            .HasColumnName("_CompetitionId");

        builder.Property(e => e.ResultId)
            .HasColumnName("_ResultId");

        builder.Property(e => e.LocationId)
            .HasColumnName("_LocationId");

        builder.HasIndex(e => e.CompetitionId);
        builder.HasIndex(e => e.LocationId);
        builder.HasIndex(e => e.Status);
    }
}

public class OneOnOneEventConfig : IEntityTypeConfiguration<OneOnOneEvent>
{
    public void Configure(EntityTypeBuilder<OneOnOneEvent> builder)
    {
        builder.HasOne(e => e.HomePlayer)
            .WithMany()
            .HasForeignKey(e => e.HomePlayerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.AwayPlayer)
            .WithMany()
            .HasForeignKey(e => e.AwayPlayerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(e => e.AwayPlayerId)
            .HasColumnName("_AwayPlayerId");

        builder.Property(e => e.HomePlayerId)
            .HasColumnName("_HomePlayerId");

        builder.ToTable(t => t.HasCheckConstraint(
            "CK_OneOnOneEvent_HomeAwayDifferent",
            "[_HomePlayerId] <> [_AwayPlayerId] OR (EventType != 0)"
        ));
    }
}

public class TeamEventConfig : IEntityTypeConfiguration<TeamEvent>
{
    public void Configure(EntityTypeBuilder<TeamEvent> builder)
    {
        builder.HasOne(e => e.HomeTeam)
            .WithMany()
            .HasForeignKey(e => e.HomeTeamId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.AwayTeam)
            .WithMany()
            .HasForeignKey(e => e.AwayTeamId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(e => e.AwayTeamId)
            .HasColumnName("_AwayTeamId");

        builder.Property(e => e.HomeTeamId)
            .HasColumnName("_HomeTeamId");

        builder.ToTable(t => t.HasCheckConstraint(
            "CK_TeamEvent_HomeAwayDifferent",
            "[_HomeTeamId] <> [_AwayTeamId] OR (EventType != 1)"
        ));
    }
}

public class FreeForAllEventConfig : IEntityTypeConfiguration<FreeForAllEvent>
{
    public void Configure(EntityTypeBuilder<FreeForAllEvent> builder)
    {
        builder.HasMany(e => e.Participants)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "FreeForAllEventParticipants",
                j => j
                    .HasOne<Player>()
                    .WithMany()
                    .HasForeignKey("_PlayerId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<FreeForAllEvent>()
                    .WithMany()
                    .HasForeignKey("_EventId")
                    .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.HasKey("_EventId", "_PlayerId");
                });
    }
}