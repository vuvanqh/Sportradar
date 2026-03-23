using Sportradar.Core.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sportradar.Core.Application.ServiceContracts;

public interface IEventService
{
    Task<List<EventResponse>> GetAllEvents();
    Task<List<EventResponse>> GetEventsBySport(Guid sportId);
    Task<List<EventResponse>> GetEventsByCity(string city);
    Task<List<EventResponse>> GetEventsByCountry(string country);
    Task<List<EventResponse>> GetEventsByLocation(Guid locationId);
    Task<List<EventResponse>> GetEventsByDateRange(DateTime startDate, DateTime endDate);
    Task<List<EventResponse>> GetEventsByCompetition(Guid competitionId);

    Task AddParticipant(Guid eventId, Guid participantId);
    Task RemoveParticipant(Guid eventId, Guid participantId);
    Task<EventResponse?> GetEventById(Guid eventId);
    Task CreateEvent(CreateEventRequest request);
    Task UpdateEvent(UpdateEventRequest request);
    Task DeleteEvent(Guid eventId);
}
