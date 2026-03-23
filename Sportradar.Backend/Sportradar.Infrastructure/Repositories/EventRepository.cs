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
        return await _context.Events
            .Include(e => e.Location)
            .Include(e => e.Sport)
            .Include(e => e.Result)
            .Include(e => e.Competition)
            .ToListAsync();
    }

    public async Task<List<Event>> GetByCityAsync(string city)
    {
        return await _context.Events.Where(e => e.Location.City == city)
            .Include(e=>e.Location)
            .Include(e=>e.Sport)
            .Include(e=>e.Result)
            .Include(e=>e.Competition)
            .ToListAsync();
    }

    public async Task<List<Event>> GetByCompetitionAsync(Guid competitionId)
    {
        return await _context.Events.Where(e => e.CompetitionId == competitionId)
            .Include(e => e.Location)
            .Include(e => e.Sport)
            .Include(e => e.Result)
            .Include(e => e.Competition)
            .ToListAsync();
    }

    public async Task<List<Event>> GetByCountryAsync(string country)
    {
        return await _context.Events.Where(e => e.Location.Country == country)
            .Include(e => e.Location)
            .Include(e => e.Sport)
            .Include(e => e.Result)
            .Include(e => e.Competition)
            .ToListAsync();
    }

    public async Task<List<Event>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.Events.Where(e => e.StartTime>=startDate && e.StartTime<=endDate)
            .Include(e => e.Location)
            .Include(e => e.Sport)
            .Include(e => e.Result)
            .Include(e => e.Competition)
            .ToListAsync();
    }

    public async Task<Event?> GetByIdAsync(Guid eventId)
    {
        return await _context.Events.Where(e => e.Id == eventId)
            .Include(e => e.Location)
            .Include(e => e.Sport)
            .Include(e => e.Result)
            .Include(e => e.Competition)
            .FirstOrDefaultAsync();
    }

    public async Task<List<Event>> GetByLocationAsync(Guid locationId)
    {
        return await _context.Events.Where(e => e.LocationId == locationId)
            .Include(e => e.Location)
            .Include(e => e.Sport)
            .Include(e => e.Result)
            .Include(e => e.Competition)
            .ToListAsync();
    }

    public async Task<List<Event>> GetBySportAsync(Guid sportId)
    {
        return await _context.Events.Where(e => e.SportId == sportId)
            .Include(e => e.Location)
            .Include(e => e.Sport)
            .Include(e => e.Result)
            .Include(e => e.Competition)
            .ToListAsync();
    }

    public async Task UpdateAsync(Event ev)
    {
        if((await _context.Events.FirstOrDefaultAsync(e => e.Id == ev.Id)) is null)
         _context.Events.Update(ev);
        await _context.SaveChangesAsync();
    }
}
