using System.Text.Json.Serialization;

namespace Sportradar.Core.Entities;

public class Player
{
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Country { get; set; }

    public Guid? TeamId {get;set;}
    [JsonIgnore]
    public SportTeam? Team { get; set; }
    [JsonIgnore]
    public ICollection<FreeForAllEvent> FreeForAllEvents { get; set; }= new List<FreeForAllEvent>();
}
