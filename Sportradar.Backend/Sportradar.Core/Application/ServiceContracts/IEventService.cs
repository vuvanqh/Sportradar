using System;
using System.Collections.Generic;
using System.Text;

namespace Sportradar.Core.Application.ServiceContracts;

public interface IEventService
{
    Task GetEventsBySport(Guid sportId);
    Task GetEventsByCity(Guid cityId);
    Task GetEventsByCountry(Guid countryId);
    Task GetEventsByDateRange(DateTime startDate, DateTime endDate);
    Task GetEventsByCompetition(Guid competitionId);

    Task GetEvenstByTeam(Guid teamId);
    Task GetEvenstByPlayer(Guid playerId);

    Task GetEventDetails(Guid eventId);

    Task CreateEvent();
    Task UpdateEvent(Guid eventId);
    Task DeleteEvent(Guid eventId);
}
