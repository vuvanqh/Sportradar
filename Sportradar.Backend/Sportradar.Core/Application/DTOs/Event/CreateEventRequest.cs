using Sportradar.Core.Entities;
using Sportradar.Core.Value_Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sportradar.Core.Application.DTOs;

public abstract class CreateEventRequest
{
    [Required] public string Title { get; init; } = null!;
    public Guid? LocationId { get; init; }
    public LocationDTO? NewLocation { get; init; }
    [Required] public DateTime StartTime { get; init; }
    [Required] public DateTime EndTime { get; init; }
    [Required] public Guid SportId { get; init; }

    public string? Description { get; init; }
    public Guid? CompetitionId { get; init; }

    public abstract Event ToEvent(Guid locationId);
}

public class CreateTeamEventRequest : CreateEventRequest
{
    [Required] public Guid HomeTeamId { get; init; }
    [Required] public Guid AwayTeamId { get; init; }

    public override Event ToEvent(Guid locationId)
    {
        return new TeamEvent
        {
            Id = Guid.NewGuid(),
            Title = Title,
            LocationId = locationId,
            StartTime = StartTime,
            EndTime = EndTime,
            SportId = SportId,
            Description = Description,
            CompetitionId = CompetitionId,
            HomeTeamId = HomeTeamId,
            AwayTeamId = AwayTeamId
        };
    }
}

public class CreateOneOnOneEventRequest : CreateEventRequest
{
    [Required] public Guid HomePlayerId { get; init; }
    [Required] public Guid AwayPlayerId { get; init; }

    public override Event ToEvent(Guid locationId)
    {
        return new OneOnOneEvent
        {
            Id = Guid.NewGuid(),
            Title = Title,
            LocationId = locationId,
            StartTime = StartTime,
            EndTime = EndTime,
            SportId = SportId,
            Description = Description,
            CompetitionId = CompetitionId,
            HomePlayerId = HomePlayerId,
            AwayPlayerId = AwayPlayerId
        };
    }
}

public class CreateFreeForAllEventRequest : CreateEventRequest
{
    [Required] public List<Guid> ParticipantIds { get; init; } = new List<Guid>();

    public override Event ToEvent(Guid locationId)
    {
        return new FreeForAllEvent
        {
            Id = Guid.NewGuid(),
            Title = Title,
            LocationId = locationId,
            StartTime = StartTime,
            EndTime = EndTime,
            SportId = SportId,
            Description = Description,
            CompetitionId = CompetitionId,
        };
    }
}
