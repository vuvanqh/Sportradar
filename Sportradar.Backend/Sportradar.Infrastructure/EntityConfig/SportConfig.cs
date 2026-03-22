using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sportradar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sportradar.Infrastructure.EntityConfig;

public class SportConfig : IEntityTypeConfiguration<Sport>
{
    public void Configure(EntityTypeBuilder<Sport> builder)
    {
        builder.HasKey(s => s.Id);
    }
}
