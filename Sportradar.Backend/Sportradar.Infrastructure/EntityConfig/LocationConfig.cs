using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sportradar.Core.Entities;
using System.Text.Json;

namespace Sportradar.Infrastructure.EntityConfig;

public class LocationConfig : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.HasKey(l => l.Id);

        builder.HasMany(l => l.Events)
            .WithOne(e => e.Location)
            .HasForeignKey(e => e.LocationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(l => l.Country);
        builder.HasIndex(l => l.City);

        var path = Path.Combine(AppContext.BaseDirectory, "seed", "locations.json");
        var json = File.ReadAllText(path);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        List<Location> locations = JsonSerializer.Deserialize<List<Location>>(json, options)!;

        builder.HasData(locations);
    }
}
