using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sportradar.Core.Entities;
using System.Text.Json;

namespace Sportradar.Infrastructure.EntityConfig;

public class SportTeamConfig : IEntityTypeConfiguration<SportTeam>
{
    public void Configure(EntityTypeBuilder<SportTeam> builder)
    {
        builder.HasKey(team => team.Id);

        builder.HasMany(team => team.Players)
            .WithOne(p => p.Team)
            .HasForeignKey(p => p.TeamId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(team => team.Name).IsUnique();
        builder.HasIndex(team => team.Id);

        var path = Path.Combine(AppContext.BaseDirectory, "seed", "teams.json");
        var json = File.ReadAllText(path);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        List<SportTeam> teams = JsonSerializer.Deserialize<List<SportTeam>>(json,options)!;

        builder.HasData(teams);
    }
}
