namespace Sportradar.Core.Entities;

public class Player
{
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Country { get; set; }

    public Guid? TeamId {get;set;}
    public SportTeam? Team { get; set; }
}
