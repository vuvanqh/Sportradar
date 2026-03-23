using System.Text.Json.Serialization;

namespace Sportradar.Core.Entities;

public enum ResultType
{
    OneOnOneResult,
    TeamResult,
    FreeForAll
}

public abstract class Result
{
    public required Guid Id { get; set; }

    //relations
    public required Guid EventId { get; set; }
    [JsonIgnore]
    public Event Event { get; set; } = null!;
}

public class TeamResult : Result
{
    public int HomeTeamScore { get; set; }
    public int AwayTeamScore { get; set; }

    //relations
    public Guid HomeTeamId { get; set; }
    [JsonIgnore]
    public SportTeam HomeTeam { get; set; } = null!;
    public Guid AwayTeamId { get; set; }
    [JsonIgnore]
    public SportTeam AwayTeam { get; set; } = null!;
}

public class OneOnOneResult : Result
{
    public int HomePlayerScore { get; set; }
    public int AwayPlayerScore { get; set; }

    //relations
    public Guid HomePlayerId { get; set; }
    [JsonIgnore]
    public Player HomePlayer { get; set; } = null!;
    public Guid AwayPlayerId { get; set; }
    [JsonIgnore]
    public Player AwayPlayer { get; set; } = null!;
}

public class FreeForAllResult : Result
{
    [JsonIgnore]
    public ICollection<FreeForAllResultEntry> Entries { get; set; } = new List<FreeForAllResultEntry>();   
}

public class  FreeForAllResultEntry
{
    public required int Score { get; set; }

    //relations 
    public required Guid PlayerId { get; set; }
    [JsonIgnore]
    public Player Player { get; set; } = null!;
    public Guid ResultId { get; set; }
    [JsonIgnore]
    public FreeForAllResult Result { get; set; } = null!;
}