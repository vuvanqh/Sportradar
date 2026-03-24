using Sportradar.Core.Entities;
using Sportradar.Core.Value_Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sportradar.Core.Application.DTOs;

public class EventResponse
{
    [Required] public Guid EventId { get; init; }
    [Required] public string Title { get; init; } = null!;
    [Required] public LocationDTO Location { get; init; } = null!;
    [Required] public DateTime StartTime { get; init; }
    [Required] public DateTime EndTime { get; init; }
    [Required] public Status Status { get; init; }
    [Required] public string SportName { get; init; } = null!;

    public string? Description { get; init; }
    public Guid? CompetitionId { get; init; }
    public string? CompetitionName { get; init; }
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
    [Required] public string HomePlayerFirstName { get; init; } = null!;
    [Required] public string HomePlayerLastName { get; init; } = null!;
    [Required] public Guid AwayPlayerId { get; init; }
    [Required] public string AwayPlayerFirstName { get; init; } = null!;
    [Required] public string AwayPlayerLastName { get; init; } = null!;
    [Required] public OneOnOneResultDTO? Result { get; init; }
}

public class FreeForAllEventResponse : EventResponse
{
    [Required] public List<PlayerPreviewDTO> Participants { get; init; } = new List<PlayerPreviewDTO>();
    [Required] public int NumberOfParticipants { get; init; }
    [Required] public FreeForAllResultDTO? Result { get; init; }
}
