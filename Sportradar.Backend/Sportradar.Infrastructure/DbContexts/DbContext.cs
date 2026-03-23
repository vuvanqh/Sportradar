using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sportradar.Core.Entities;
using Sportradar.Infrastructure.EntityConfig;

namespace Sportradar.Infrastructure;

public class ApplicationDbContext: DbContext
{
    public DbSet<Sport> Sports => Set<Sport>();
    public DbSet<SportTeam> SportTeams => Set<SportTeam>();
    public DbSet<Player> Players => Set<Player>();
    public DbSet<Location> Locations => Set<Location>();
    public DbSet<Event> Events => Set<Event>();
    public DbSet<Result> Results => Set<Result>();
    public DbSet<Competition> Competitions => Set<Competition>();
    public DbSet<FreeForAllResultEntry> FreeForAllResultEntries => Set<FreeForAllResultEntry>();

    public ApplicationDbContext(DbContextOptions options): base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.ApplyConfiguration(new EventConfig());
        //modelBuilder.ApplyConfiguration(new TeamEventConfig());

        //modelBuilder.ApplyConfiguration(new FreeForAllEventConfig());
        //modelBuilder.ApplyConfiguration(new OneOnOneEventConfig());
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
