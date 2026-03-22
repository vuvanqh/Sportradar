using System;
using System.Collections.Generic;
using System.Text;

namespace Sportradar.Core.Entities;

public enum CompetitionType
{
    Team,
    Individual
}

public class Competition
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public virtual required CompetitionType Type { get; set; }

    //relations
    public Guid SportId { get; set; }
    public Sport Sport { get; set; } = null!;

    public ICollection<Event> Events { get; set; } = new List<Event>();
}

public class TeamCompetition : Competition
{
    public override required CompetitionType Type { get; set; } = CompetitionType.Team;
    public ICollection<SportTeam> Teams { get; set; } = new List<SportTeam>();  
}
public class IndividualCompetition : Competition
{
    public override required CompetitionType Type { get; set; } = CompetitionType.Individual;
    public ICollection<Player> Participants { get; set; } = new List<Player>();
}
