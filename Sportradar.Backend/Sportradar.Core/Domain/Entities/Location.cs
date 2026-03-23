using System.Text.Json.Serialization;

namespace Sportradar.Core.Entities;

public class Location
{
    public required Guid Id { get; set; }
    public required string Country { get; set; }
    public required string City { get; set; }
    public string? Venue { get; set; }

    //relations
    [JsonIgnore]
    public ICollection<Event> Events { get; set; } = new List<Event>();
}
