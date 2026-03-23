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
    public Event Event { get; set; } = null!;
}

public class TeamResult : Result
{
    public int HomeTeamScore { get; set; }
    public int AwayTeamScore { get; set; }

    //relations
    public Guid HomeTeamId { get; set; }
    public SportTeam HomeTeam { get; set; } = null!;
    public Guid AwayTeamId { get; set; }
    public SportTeam AwayTeam { get; set; } = null!;
}

public class OneOnOneResult : Result
{
    public int HomePlayerScore { get; set; }
    public int AwayPlayerScore { get; set; }

    //relations
    public Guid HomePlayerId { get; set; }
    public Player HomePlayer { get; set; } = null!;
    public Guid AwayPlayerId { get; set; }
    public Player AwayPlayer { get; set; } = null!;
}

public class FreeForAllResult : Result
{
    public ICollection<FreeForAllResultEntry> Entries { get; set; } = new List<FreeForAllResultEntry>();   
}

public class  FreeForAllResultEntry
{
    public required int Score { get; set; }

    //relations 
    public required Guid PlayerId { get; set; }
    public Player Player { get; set; } = null!;
    public Guid ResultId { get; set; }
    public FreeForAllResult Result { get; set; } = null!;
}