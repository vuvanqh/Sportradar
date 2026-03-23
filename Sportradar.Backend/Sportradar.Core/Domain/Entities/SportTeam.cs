namespace Sportradar.Core.Entities;

public class SportTeam
{
    public required Guid Id { get; set; }
    public required Guid SportId { get; set; }
    public required string Name { get; set; }
    public required string OfficialName { get; set; }
    public required string Abbreviation { get; set; }   

    //relations
    public ICollection<Player> Players { get; set; } = new List<Player>();
}
