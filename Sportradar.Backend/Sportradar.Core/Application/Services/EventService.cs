using Sportradar.Core.Application.DTOs;
using Sportradar.Core.Application.ServiceContracts;
using Sportradar.Core.Domain;
using Sportradar.Core.Domain.RepositoryContracts;
using Sportradar.Core.Entities;
using System.Diagnostics.Metrics;

namespace Sportradar.Core.Application.Services;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;
    private readonly ILocationRepository _locationRepository;
    private readonly IPlayerRepository _playerRepository;
    public EventService(IEventRepository eventRepository, ILocationRepository locationRepository, IPlayerRepository playerRepository)
    {
        _eventRepository = eventRepository;
        _locationRepository = locationRepository;   
        _playerRepository = playerRepository;
    }

    public async Task AddParticipant(Guid eventId, Guid participantId)
    {
        Event e = await _eventRepository.GetByIdAsync(eventId)?? throw new ArgumentException("Event does not exist.");
        if(e is not FreeForAllEvent)
        {
            throw new ArgumentException("Participants can only be added to FreeForAll events.");
        }
        FreeForAllEvent freeForAllEvent = (FreeForAllEvent)e;
        Player p = await _playerRepository.GetByIdAsync(participantId)?? throw new ArgumentException("Player does not exist.");
        
        if(freeForAllEvent.Participants.Any(p => p.Id == participantId))
        {
            throw new ArgumentException("Player is already a participant of this event.");
        }

        freeForAllEvent.Participants.Add(p);
        await _eventRepository.UpdateAsync(freeForAllEvent);
    }
    public async Task RemoveParticipant(Guid eventId, Guid participantId)
    {
        Event e = await _eventRepository.GetByIdAsync(eventId) ?? throw new ArgumentException("Event does not exist.");
        if (e is not FreeForAllEvent)
        {
            throw new ArgumentException("Participants can only be added to FreeForAll events.");
        }
        FreeForAllEvent freeForAllEvent = (FreeForAllEvent)e;
        Player p = freeForAllEvent.Participants.FirstOrDefault(p => p.Id == participantId) ?? throw new ArgumentException("Player is not a participant of this event.");
        freeForAllEvent.Participants.Remove(p);
        await _eventRepository.UpdateAsync(freeForAllEvent);
    }

    public async Task CreateEvent(CreateEventRequest request)
    {
        if (request.StartTime >= request.EndTime)
        {
            throw new ArgumentException("Start time must be before end time.");
        }
        if(request.LocationId == null && request.NewLocation is null)
        {
            throw new ArgumentException("Either LocationId or NewLocation must be provided.");
        }
        Location location =  await GetLocation(request.LocationId, request.NewLocation);
        await _eventRepository.AddAsync(request.ToEvent(location.Id));
    }

    public async Task DeleteEvent(Guid eventId)
    {
        Event e = (await _eventRepository.GetByIdAsync(eventId))?? throw new ArgumentException("Event does not exist.");
        await _eventRepository.DeleteAsync(eventId);
    }

    public async Task<EventResponse?> GetEventById(Guid eventId)
    {
        Event? e = await _eventRepository.GetByIdAsync(eventId);
        return e==null?null:EventMapper.EventToEventResponse(e);
    }

    public async Task<List<EventResponse>> GetEventsByCity(string city)
    {
        return (await _eventRepository.GetByCityAsync(city))
            .Select(e => EventMapper.EventToEventResponse(e))
            .ToList();
    }

    public async Task<List<EventResponse>> GetEventsByCompetition(Guid competitionId)
    {
        return (await _eventRepository.GetByCompetitionAsync(competitionId))
            .Select(e => EventMapper.EventToEventResponse(e))
            .ToList();
    }

    public async Task<List<EventResponse>> GetEventsByCountry(string country)
    {
        return (await _eventRepository.GetByCountryAsync(country))
            .Select(e => EventMapper.EventToEventResponse(e))
            .ToList();
    }

    public async Task<List<EventResponse>> GetEventsByDateRange(DateTime startDate, DateTime endDate)
    {
        return (await _eventRepository.GetByDateRangeAsync(startDate, endDate))
            .Select(e => EventMapper.EventToEventResponse(e))
            .ToList();
    }

    public async Task<List<EventResponse>> GetEventsByLocation(Guid locationId)
    {
        return (await _eventRepository.GetByLocationAsync(locationId))
            .Select(e => EventMapper.EventToEventResponse(e))
            .ToList();
    }

    public async Task<List<EventResponse>> GetEventsBySport(Guid sportId)
    {
        return (await _eventRepository.GetBySportAsync(sportId))
            .Select(e => EventMapper.EventToEventResponse(e))
            .ToList();
    }

    public async Task UpdateEvent(UpdateEventRequest request)
    {
        Event e = await _eventRepository.GetByIdAsync(request.EventId) ?? throw new ArgumentException("Event does not exist.");
        
        switch(e)
        {
            case OneOnOneEvent oneOnOne:
                {
                    if (request is not UpdateOneOnOneEventRequest req)
                        throw new ArgumentException("Invalid request type");

                    if (req.AwayPlayerId is not null) oneOnOne.AwayPlayerId = req.AwayPlayerId.Value;
                    if (req.HomePlayerId is not null)  oneOnOne.HomePlayerId = req.HomePlayerId.Value;
                    //await _eventRepository.UpdateAsync(oneOnOne);
                    break;
                }
            case TeamEvent oneOnOne:
                {
                    if (request is not UpdateTeamEventRequest req)
                        throw new ArgumentException("Invalid request type");

                    if (req.AwayTeamId is not null) oneOnOne.AwayTeamId = req.AwayTeamId.Value;
                    if (req.HomeTeamId is not null) oneOnOne.HomeTeamId = req.HomeTeamId.Value;
                    //await _eventRepository.UpdateAsync(oneOnOne);
                    break;
                }
            default:
                throw new ArgumentException("Unknown event type.");
        }
        if(request.StartTime is not null) e.StartTime = request.StartTime.Value;
        if(request.EndTime is not null) e.EndTime = request.EndTime.Value;
        if(request.Title is not null) e.Title = request.Title;
        if(request.Description is not null) e.Description = request.Description;
        if(request.LocationId is not null || request.NewLocation is not null)
        {
            Location l = await GetLocation(request.LocationId, request.NewLocation);
            e.LocationId = l.Id;
        }
        if(e.StartTime>e.EndTime)
        {
            throw new ArgumentException("Start time must be before end time.");
        }

        await _eventRepository.UpdateAsync(e);
    }


    private async Task<Location> GetLocation(Guid? locationId,LocationDTO? locationDTO)
    {
        Location l;
        if (locationId == null && locationDTO != null)
        {
            l = new Location
            {
                Id = Guid.NewGuid(),
                City = locationDTO.City,
                Country = locationDTO.Country,
                Venue = locationDTO.Venue
            };
            await _locationRepository.CreateAsync(l);
        }
        else
        {
            l = (await _locationRepository.GetByIdAsync(locationId!.Value)) ?? throw new ArgumentException("Location does not exist.");
        }
        return l;
    }

    public async Task<List<EventResponse>> GetAllEvents()
    {
        return (await _eventRepository.GetAllAsync()).Select(EventMapper.EventToEventResponse).ToList();
    }
}
