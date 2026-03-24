using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sportradar.Core.Application.DTOs;

public record TeamResponse
{
    [Required] public Guid TeamId { get; init; }
    [Required] public string TeamName { get; init; } = null!;
    [Required] public string OfficialName { get; init; } = null!;
    [Required] public string Abbreviation { get; init; } = null!;
    [Required] public Guid SportId { get; init; }
}

