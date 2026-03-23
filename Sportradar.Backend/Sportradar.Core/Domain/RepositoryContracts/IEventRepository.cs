using Sportradar.Core.Entities;

namespace Sportradar.Core.Domain;

public interface IEventRepository
{
    Task<List<Event>> GetBySportAsync(Guid sportId);
    Task<List<Event>> GetByCityAsync(string city);
    Task<List<Event>> GetByCountryAsync(string country);
    Task<List<Event>> GetByLocationAsync(Guid locationId);
    Task<List<Event>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<List<Event>> GetByCompetitionAsync(Guid competitionId);
    Task<Event?> GetByIdAsync(Guid eventId);
    Task AddAsync(Event ev);
    Task UpdateAsync(Event ev);
    Task DeleteAsync(Guid eventId);
}
