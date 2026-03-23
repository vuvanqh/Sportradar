using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sportradar.Core.Entities;
using System.Text.Json;

namespace Sportradar.Infrastructure.EntityConfig;

public class ResultConfig: IEntityTypeConfiguration<Result>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Result> builder)
    {
        builder.HasKey(r => r.Id);
        builder.HasDiscriminator<ResultType>("ResultType")
            .HasValue<OneOnOneResult>(ResultType.OneOnOneResult)
            .HasValue<TeamResult>(ResultType.TeamResult)
            .HasValue<FreeForAllResult>(ResultType.FreeForAll);

        builder.HasIndex(r => r.EventId).IsUnique();
    }
}

public class OneOnOneResultConfig : IEntityTypeConfiguration<OneOnOneResult>
{
    public void Configure(EntityTypeBuilder<OneOnOneResult> builder)
    {
        builder.HasOne(r => r.HomePlayer)
            .WithMany()
            .HasForeignKey(r => r.HomePlayerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.AwayPlayer)
            .WithMany()
            .HasForeignKey(r => r.AwayPlayerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(e => e.AwayPlayerId)
            .HasColumnName("_AwayPlayerId");

        builder.Property(e => e.HomePlayerId)
            .HasColumnName("_HomePlayerId");

        builder.ToTable(t => t.HasCheckConstraint(
            "CK_OneOnOneResult_HomeAwayDifferent",
            "[_HomePlayerId] <> [_AwayPlayerId] OR (ResultType != 0)"
        ));
        builder.ToTable(t => t.HasCheckConstraint(
           "CK_OneOnOneResult_Score_NonNegative",
           "([HomePlayerScore] >= 0 AND [AwayPlayerScore] >= 0) OR (ResultType != 0)"
       ));

        builder.HasIndex(r => r.HomePlayerId);
        builder.HasIndex(r => r.AwayPlayerId);

    }
}

public class TeamResultConfig : IEntityTypeConfiguration<TeamResult>
{
    public void Configure(EntityTypeBuilder<TeamResult> builder)
    {
        builder.HasOne(r => r.HomeTeam)
            .WithMany()
            .HasForeignKey(r => r.HomeTeamId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.AwayTeam)
            .WithMany()
            .HasForeignKey(r => r.AwayTeamId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.Property(e => e.AwayTeamId)
            .HasColumnName("_AwayTeamId");

        builder.Property(e => e.HomeTeamId)
            .HasColumnName("_HomeTeamId");

        builder.ToTable(t => t.HasCheckConstraint(
           "CK_TeamResult_HomeAwayDifferent",
           "[_HomeTeamId] <> [_AwayTeamId] OR (ResultType != 1)"
       ));
        builder.ToTable(t => t.HasCheckConstraint(
           "CK_TeamResult_Score_NonNegative",
            "([HomeTeamScore] >= 0 AND [AwayTeamScore] >= 0) OR (ResultType != 1)"
        ));

        builder.HasIndex(r => r.HomeTeamId);
        builder.HasIndex(r => r.AwayTeamId);

  
    }
}

public class FreeForAllResultConfig : IEntityTypeConfiguration<FreeForAllResult>
{
    public void Configure(EntityTypeBuilder<FreeForAllResult> builder)
    {

    }
}

public class FreeForAllResultEntryConfig : IEntityTypeConfiguration<FreeForAllResultEntry>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<FreeForAllResultEntry> builder)
    {
        builder.HasKey(r => new { r.ResultId, r.PlayerId });

        builder.HasOne(r => r.Result)
            .WithMany(r => r.Entries)
            .HasForeignKey(r => r.ResultId);

        builder.HasOne(e => e.Player)
            .WithMany()
            .HasForeignKey(e => e.PlayerId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.Property(e => e.ResultId)
            .HasColumnName("_ResultId");

        builder.Property(e => e.PlayerId)
            .HasColumnName("_PlayerId");

        builder.ToTable(t => t.HasCheckConstraint(
           "CK_FreeForAllResultEntry_Score_NonNegative",
           "[Score] >= 0"
       ));

        builder.HasIndex(r => r.ResultId);
        builder.HasIndex(r => r.PlayerId);

    
    }
}


