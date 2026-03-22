using Microsoft.EntityFrameworkCore;
using Sportradar.Core.Entities;

namespace Sportradar.Infrastructure.EntityConfig;

public class ResultConfig: IEntityTypeConfiguration<Result>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Result> builder)
    {
        builder.HasKey(r => r.Id);
        builder.HasDiscriminator<EventType>("ResultType")
            .HasValue<ExclusiveResult>(EventType.Team)
            .HasValue<ExclusiveResult>(EventType.OneOnOne)
            .HasValue<FreeForAllResult>(EventType.FreeForAll);
    }
}
