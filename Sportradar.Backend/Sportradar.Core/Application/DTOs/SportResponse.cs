using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sportradar.Core.Application.DTOs;

public record SportResponse
{
    [Required]  public Guid SportId { get; init; }
    [Required]  public string Name { get; init; } = null!;
}
