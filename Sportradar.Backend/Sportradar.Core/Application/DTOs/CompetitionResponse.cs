using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sportradar.Core.Application.DTOs;

public class CompetitionResponse
{
    [Required] public Guid Id { get; init; }
    [Required] public string Name { get; init; } = null!;
    [Required] public string SportName { get; init; } = null!;
}
