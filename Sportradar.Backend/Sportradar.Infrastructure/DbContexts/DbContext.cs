using Microsoft.EntityFrameworkCore;
using Sportradar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sportradar.Infrastructure;

public class ApplicationDbContext: DbContext
{
    public DbSet<Sport> Sports => Set<Sport>();
    public DbSet<SportTeam> SportTeams => Set<SportTeam>();
    public DbSet<Player> PlayerTeams => Set<Player>();
    public DbSet<Location> Locations => Set<Location>();
    public DbSet<Event> Events => Set<Event>();
    public DbSet<Result> Results => Set<Result>();
    public DbSet<Competition> Competitions => Set<Competition>();

    public ApplicationDbContext(DbContextOptions options): base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
