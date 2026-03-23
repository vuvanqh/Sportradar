namespace Sportradar.Core.Entities;
public enum CompetitionType
{
    Team,
    OneOnOne
}

public abstract class Competition
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }

    //relations
    public Guid SportId { get; set; }
    public Sport Sport { get; set; } = null!;

    public ICollection<Event> Events { get; set; } = new List<Event>();
}

public class TeamCompetition : Competition
{
    public ICollection<SportTeam> Teams { get; set; } = new List<SportTeam>();  
}
public class OneOnOneCompetition : Competition
{
    public ICollection<Player> Participants { get; set; } = new List<Player>();
}
