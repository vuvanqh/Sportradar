using Sportradar.Core.Application.DTOs;
using Sportradar.Core.Entities;
using Sportradar.Core.Value_Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sportradar.Core.Application;

public static class EventMapper
{
    public static EventResponse EventToEventResponse(Event e)
    {
        return e switch
        {
            OneOnOneEvent oneOnOneEvent => OneOnOneMap(oneOnOneEvent),
            TeamEvent teamEvent => TeamEventMap(teamEvent),
            FreeForAllEvent freeForAllEvent => FreeForAllMap(freeForAllEvent),
            _ => throw new ArgumentException("Unknown event type")
        };
    }
    private static OneOnOneEventResponse OneOnOneMap(OneOnOneEvent e)
    {
        OneOnOneResult? oneOnOneResult = e.Result as OneOnOneResult;
        return new OneOnOneEventResponse
        {
            EventId = e.Id,
            Title = e.Title,
            Location = new LocationDTO()
            {
                Venue = e.Location.Venue,
                City = e.Location.City,
                Country = e.Location.Country
            },
            StartTime = e.StartTime,
            EndTime = e.EndTime,
            Status = e.Status,
            SportName = e.Sport.Name,
            Description = e.Description,
            CompetitionId = e.Competition?.Id,
            CompetitionName = e.Competition?.Name,
            HomePlayerId = e.HomePlayerId,
            HomePlayerFirstName = e.HomePlayer.FirstName,
            HomePlayerLastName = e.HomePlayer.LastName,
            AwayPlayerId = e.AwayPlayerId,
            AwayPlayerFirstName = e.AwayPlayer.FirstName,
            AwayPlayerLastName = e.AwayPlayer.LastName,
            Result = oneOnOneResult != null ? new OneOnOneResultDTO()
            {
                HomePlayerScore = oneOnOneResult!.HomePlayerScore,
                AwayPlayerScore = oneOnOneResult!.AwayPlayerScore
            } : null
        };
    }
        
    private static FreeForAllEventResponse FreeForAllMap(FreeForAllEvent e)
    {
        FreeForAllResult? freeForAllResult = e.Result as FreeForAllResult;
        return new FreeForAllEventResponse
        {
            EventId = e.Id,
            Title = e.Title,
            Location = new LocationDTO()
            {
                Venue = e.Location.Venue,
                City = e.Location.City,
                Country = e.Location.Country
            },
            StartTime = e.StartTime,
            EndTime = e.EndTime,
            Status = e.Status,
            SportName = e.Sport.Name,
            Description = e.Description,
            CompetitionId = e.Competition?.Id,
            CompetitionName = e.Competition?.Name,
            Result = freeForAllResult != null ? new FreeForAllResultDTO()
            {
                Results = freeForAllResult!.Entries.Select(e => new FreeForAllResultEntryDTO()
                {
                    PlayerId = e.PlayerId,
                    FirstName = e.Player.LastName,
                    LastName = e.Player.FirstName
                }).ToList(),
                NumberOfParticipants = freeForAllResult!.Entries.Count
            } : null
        };
    }
    private static TeamEventResponse TeamEventMap(TeamEvent e)
    {
        TeamResult? teamResult = e.Result as TeamResult;
        return new TeamEventResponse
        {
            EventId = e.Id,
            Title = e.Title,
            Location = new LocationDTO()
            {
                Venue = e.Location.Venue,
                City = e.Location.City,
                Country = e.Location.Country
            },
            StartTime = e.StartTime,
            EndTime = e.EndTime,
            Status = e.Status,
            SportName = e.Sport.Name,
            Description = e.Description,
            CompetitionId = e.Competition?.Id,
            CompetitionName = e.Competition?.Name,
            HomeTeamId = e.HomeTeamId,
            HomeTeamName = e.HomeTeam.Name,
            AwayTeamId = e.AwayTeamId,
            AwayTeamName = e.AwayTeam.Name,
            Result = teamResult != null ? new TeamResultDTO()
            {
                HomeTeamScore = teamResult!.HomeTeamScore,
                AwayTeamScore = teamResult!.AwayTeamScore
            } : null
        };
    }
}
