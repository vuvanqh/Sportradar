using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sportradar.Core.Entities;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sportradar.Infrastructure.EntityConfig;

public class SportConfig : IEntityTypeConfiguration<Sport>
{
    public void Configure(EntityTypeBuilder<Sport> builder)
    {
        builder.HasKey(s => s.Id);


        var path = Path.Combine(AppContext.BaseDirectory, "seed", "sports.json");
        var json = File.ReadAllText(path);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        List<Sport> sports = JsonSerializer.Deserialize<List<Sport>>(json, options)!;

        builder.HasData(sports);
    }
}
