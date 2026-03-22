using Sportradar.Core.Value_Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sportradar.Core.Entities;

public enum EventType
{
    OneOnOne,
    Team,
    FreeForAll
}

public class Event
{
    public required Guid Id { get; set; }
    public required DateTime Date { get; set; }
    public DateTime CreatedAt { get; set; }
    public Status Status { get; set; }
    public virtual required EventType Type { get; set; }    

    //relations
    public Guid LocationId { get; set; }
    public Location Location { get; set; } = null!;
    public Guid ResultId { get; set; }
    public Result? Result { get; set; }
    public Guid CompetitionId { get; set; }
    public Competition? Competition { get; set; }
}

public class TeamEvent: Event
{
    public override required EventType Type { get; set; } = EventType.Team;
    public Guid HomeId { get; set; }
    public SportTeam HomeTeam { get; set; } = null!;
    public Guid AwayId { get; set; }
    public SportTeam AwayTeam { get; set; } = null!;
}

public class FreeForAllEvent : Event
{
    public override required EventType Type { get; set; } = EventType.FreeForAll;
    public ICollection<Player> Participants { get; set; } = new List<Player>();
}

public class OneOnOneEvent : Event
{
    public override required EventType Type { get; set; } = EventType.OneOnOne;
    public Guid HomeId { get; set; }
    public Player HomePlayer { get; set; } = null!;
    public Guid AwayId { get; set; }
    public Player AwayPlayer { get; set; } = null!;
}