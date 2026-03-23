using Sportradar.Core.Application.DTOs;
using Sportradar.Core.Value_Objects;

namespace Sportradar.Core.Entities;

public enum EventType
{
    OneOnOne,
    Team,
    FreeForAll
}

public abstract class Event
{
    public required Guid Id { get; set; }
    public required DateTime StartTime { get; set; }
    public required DateTime EndTime { get; set; }
    public required string Title { get; set; }
    public DateTime CreatedAt { get; set; }
    public Status Status { get; set; }
    public string? Description { get; set; }

    //relations
    public Guid SportId { get; set; }
    public Sport Sport { get; set; } = null!;
    public Guid LocationId { get; set; }
    public Location Location { get; set; } = null!;
    public Guid? ResultId { get; set; }
    public Result? Result { get; set; }
    public Guid? CompetitionId { get; set; }
    public Competition? Competition { get; set; }
}

public class TeamEvent: Event
{
    public Guid HomeTeamId { get; set; }
    public SportTeam HomeTeam { get; set; } = null!;
    public Guid AwayTeamId { get; set; }
    public SportTeam AwayTeam { get; set; } = null!;
}

public class FreeForAllEvent : Event
{
    public ICollection<Player> Participants { get; set; } = new List<Player>();
}

public class OneOnOneEvent : Event
{
    public Guid HomePlayerId { get; set; }
    public Player HomePlayer { get; set; } = null!;
    public Guid AwayPlayerId { get; set; }
    public Player AwayPlayer { get; set; } = null!;
}