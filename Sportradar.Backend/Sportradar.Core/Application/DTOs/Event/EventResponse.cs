using Sportradar.Core.Entities;
using Sportradar.Core.Value_Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sportradar.Core.Application.DTOs.Event;

public class EventResponse
{
    [Required] public Guid EventId { get; init; }
    [Required] public string Title { get; init; } = null!;
    [Required] public LocationDTO Location { get; init; } = null!;
    [Required] public DateTime StartTime { get; init; }
    [Required] public DateTime EndTime { get; init; }
    [Required] public Status Status { get; init; }
    [Required] public string? Description { get; init; }

    [Required] public string SportName { get; init; } = null!;

    [Required] public Guid CompetitionId { get; init; }
    [Required] public string? CompetitionName { get; init; }
}

public class TeamEventResponse : EventResponse
{
    [Required] public Guid HomeTeamId { get; init; }
    [Required] public string HomeTeamName { get; init; } = null!;
    [Required] public Guid AwayTeamId { get; init; }
    [Required] public string AwayTeamName { get; init; } = null!;
    [Required] public TeamResultDTO? Result { get; init; }
}

public class OneOnOneEventResponse : EventResponse
{
    [Required] public Guid HomePlayerId { get; init; }
    [Required] public string HomePlayerName { get; init; } = null!;
    [Required] public Guid AwayPlayerId { get; init; }
    [Required] public string AwayPlayerName { get; init; } = null!;
    [Required] public OneOnOneResultDTO? Result { get; init; }
}

public class FreeForAllEventResponse : EventResponse
{
    [Required] public List<Player> Participants { get; init; } = new List<Player>();
    [Required] public int NumberOfParticipants { get; init; }
    [Required] public FreeForAllResultDTO? Result { get; init; }
}
