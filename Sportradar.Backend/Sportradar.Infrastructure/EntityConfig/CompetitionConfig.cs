using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sportradar.Core.Entities;


namespace Sportradar.Infrastructure.EntityConfig;

public class CompetitionConfig : IEntityTypeConfiguration<Competition>
{
    public void Configure(EntityTypeBuilder<Competition> builder)
    {
        builder.HasIndex(c => c.Id);

        builder.HasDiscriminator<CompetitionType>("CompetitionType")
            .HasValue<TeamCompetition>(CompetitionType.Team)
            .HasValue<IndividualCompetition>(CompetitionType.Individual);
    }
}
