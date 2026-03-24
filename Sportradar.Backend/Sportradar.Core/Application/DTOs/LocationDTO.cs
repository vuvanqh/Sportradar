using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sportradar.Core.Application.DTOs;

public record LocationDTO
{
    [Required] public Guid LocationId { get; init; }
    [Required] public string Country { get; init; } = null!;
    [Required] public string City { get; init; } = null!;
    public string? Venue { get; init; }
}
