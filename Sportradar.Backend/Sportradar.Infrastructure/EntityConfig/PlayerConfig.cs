using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sportradar.Core.Entities;
using System.Text.Json;

namespace Sportradar.Infrastructure.EntityConfig;

public class PlayerConfig : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.TeamId)
            .HasColumnName("_TeamId");

        var path = Path.Combine(AppContext.BaseDirectory, "seed", "players.json");
        var json = File.ReadAllText(path);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        List<Player> players = JsonSerializer.Deserialize<List<Player>>(json, options)!;

        builder.HasData(players);
    }
}
