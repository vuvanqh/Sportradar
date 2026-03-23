using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sportradar.Core.Entities;
using System.Text.Json;


namespace Sportradar.Infrastructure.EntityConfig;

public class CompetitionConfig : IEntityTypeConfiguration<Competition>
{
    public void Configure(EntityTypeBuilder<Competition> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasDiscriminator<CompetitionType>("CompetitionType")
            .HasValue<TeamCompetition>(CompetitionType.Team)
            .HasValue<OneOnOneCompetition>(CompetitionType.OneOnOne);

        builder.HasOne(c => c.Sport)
            .WithMany()
            .HasForeignKey(c=> c.SportId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

public class TeamCompetitionConfig : IEntityTypeConfiguration<TeamCompetition>
{
    public void Configure(EntityTypeBuilder<TeamCompetition> builder)
    {
        builder.HasMany(e => e.Teams)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "CompetitionTeams",
                j => j
                    .HasOne<SportTeam>()
                    .WithMany()
                    .HasForeignKey("TeamId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<TeamCompetition>()
                    .WithMany()
                    .HasForeignKey("CompetitionId")
                    .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.HasKey("CompetitionId", "TeamId");
                });

        var path = Path.Combine(AppContext.BaseDirectory, "seed", "competitions", "teamCompetitions.json");
        var json = File.ReadAllText(path);
        List<TeamCompetition> comp = JsonSerializer.Deserialize<List<TeamCompetition>>(json)!;

        builder.HasData(comp);
    }
}

public class OneOnOneCompetitionConfig : IEntityTypeConfiguration<OneOnOneCompetition>
{
    public void Configure(EntityTypeBuilder<OneOnOneCompetition> builder)
    {
        builder.HasMany(e => e.Participants)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "CompetitionPlayers",
                j => j
                    .HasOne<Player>()
                    .WithMany()
                    .HasForeignKey("PlayerId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<OneOnOneCompetition>()
                    .WithMany()
                    .HasForeignKey("CompetitionId")
                    .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.HasKey("CompetitionId", "PlayerId");
                });

        var path = Path.Combine(AppContext.BaseDirectory, "seed", "competitions", "oneOnOneCompetitions.json");
        var json = File.ReadAllText(path);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        List<OneOnOneCompetition> comp = JsonSerializer.Deserialize<List<OneOnOneCompetition>>(json, options)!;

        builder.HasData(comp);
    }
}