using System;
using System.Collections.Generic;
using System.Text;

namespace Sportradar.Core.Entities;

public class Result
{
    public required Guid Id { get; set; }

    //relations
    public required Guid EventId { get; set; }
    public Event Event { get; set; } = null!;
}

public class ExclusiveResult : Result
{
    public Guid HomeId { get; set; }
    public int HomeScore { get; set; }
    public Guid AwayId { get; set; }
    public int AwayScore { get; set; }
}

public class FreeForAllResult : Result
{
    public ICollection<(Guid ParticipantId, int Score)> Participants { get; set; } = new List<(Guid, int)>();
}
  