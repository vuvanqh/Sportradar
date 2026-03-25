using Microsoft.EntityFrameworkCore;
using Sportradar.Core.Domain;
using Sportradar.Core.Entities;
using System.Diagnostics.Metrics;

namespace Sportradar.Infrastructure.Repositories;

public class EventRepository : IEventRepository
{
    private readonly ApplicationDbContext _context;
    public EventRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Event ev)
    {
        var conflictExists = await _context.Events.AnyAsync(e =>
        e.LocationId == ev.LocationId &&
        e.StartTime < ev.EndTime &&
        e.EndTime > ev.StartTime);

        if (conflictExists)
            throw new InvalidOperationException("Event is already scheduled at that time and location");

        await _context.Events.AddAsync(ev);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid eventId)
    {
        if(await _context.Events.FirstOrDefaultAsync(e => e.Id == eventId) is Event ev)
        {
            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Event>> GetAllAsync()
    {
        return await QueryAll(_context.Events);
    }

    public async Task<List<Event>> GetByCityAsync(string city)
    {
        return await QueryAll(_context.Events.Where(e => e.Location.City == city));
    }

    public async Task<List<Event>> GetByCompetitionAsync(Guid competitionId)
    {
        return await QueryAll(_context.Events.Where(e => e.CompetitionId == competitionId));
    }

    public async Task<List<Event>> GetByCountryAsync(string country)
    {
        return await QueryAll(_context.Events.Where(e => e.Location.Country == country));
    }
    public async Task<List<Event>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await QueryAll(_context.Events.Where(e => e.StartTime >= startDate && e.StartTime <= endDate));
    }

    public async Task<Event?> GetByIdAsync(Guid eventId)
    {
        var baseQuery = _context.Events
            .Include(e => e.Location)
            .Include(e => e.Sport)
            .Include(e => e.Result)
            .Include(e => e.Competition);

        var team = await baseQuery
        .OfType<TeamEvent>()
        .Include(e => e.HomeTeam)
        .Include(e => e.AwayTeam)
        .FirstOrDefaultAsync(e => e.Id == eventId);

        if (team != null) return team;

        var one = await baseQuery
            .OfType<OneOnOneEvent>()
            .Include(e => e.HomePlayer)
            .Include(e => e.AwayPlayer)
            .FirstOrDefaultAsync(e => e.Id == eventId);

        if (one != null) return one;

        var free = await baseQuery
            .OfType<FreeForAllEvent>()
            .Include(e => e.Participants)
            .FirstOrDefaultAsync(e => e.Id == eventId);

        return free;
    }

    public async Task<List<Event>> GetByLocationAsync(Guid locationId)
    {
        return await QueryAll(_context.Events.Where(e => e.LocationId == locationId));
    }

    public async Task<List<Event>> GetBySportAsync(Guid sportId)
    {
        return await QueryAll(_context.Events.Where(e => e.SportId == sportId));
    }

    public async Task UpdateAsync(Event ev)
    {
        if((await _context.Events.FirstOrDefaultAsync(e => e.Id == ev.Id)) is null)
         _context.Events.Update(ev);
        await _context.SaveChangesAsync();
    }

    private IQueryable<TeamEvent> TeamEventQuery(IQueryable<Event> baseQuery)
    {
        return baseQuery
            .OfType<TeamEvent>()
            .Include(e => e.HomeTeam)
            .Include(e => e.AwayTeam);
    }
    private IQueryable<OneOnOneEvent> OneOnOneEventQuery(IQueryable<Event> baseQuery)
    {
        return baseQuery
            .OfType<OneOnOneEvent>()
            .Include(e => e.HomePlayer)
            .Include(e => e.AwayPlayer);
    }
    private IQueryable<FreeForAllEvent> FreeForAllEventQuery(IQueryable<Event> baseQuery)
    {
        return baseQuery
            .OfType<FreeForAllEvent>()
            .Include(e => e.Participants);
    }

    private async Task<List<Event>> QueryAll(IQueryable<Event> baseQuery)
    {
        IQueryable<Event> query = baseQuery
            .Include(e => e.Location)
            .Include(e => e.Sport)
            .Include(e => e.Result)
            .Include(e => e.Competition);

        var oneOnOneEvent = await OneOnOneEventQuery(query).ToListAsync();
        var teamEvent = await TeamEventQuery(query).ToListAsync();
        var freeForAllEvent = await FreeForAllEventQuery(query).ToListAsync();

        return teamEvent.Cast<Event>()
            .Concat(oneOnOneEvent)
            .Concat(freeForAllEvent)
            .ToList();
    }
}
