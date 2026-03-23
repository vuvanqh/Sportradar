using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sportradar.Core.Application.DTOs;

public class UpdateEventRequest
{
    [Required] public Guid EventId { get; init; }
    public string? Title { get; init; }
    public Guid? LocationId { get; init; }
    public LocationDTO? NewLocation { get; init; }
    public DateTime? StartTime { get; init; }
    public DateTime? EndTime { get; init; }

    public string? Description { get; init; }
}

public class UpdateTeamEventRequest : UpdateEventRequest
{
    public Guid? HomeTeamId { get; init; }
    public Guid? AwayTeamId { get; init; }
}

public class UpdateOneOnOneEventRequest : UpdateEventRequest
{
    public Guid? HomePlayerId { get; init; }
    public Guid? AwayPlayerId { get; init; }
}
