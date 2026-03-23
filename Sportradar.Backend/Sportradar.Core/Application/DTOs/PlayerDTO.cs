using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sportradar.Core.Application.DTOs;

public record PlayerPreviewDTO
{
    [Required] public Guid Id { get; init; }
    [Required] public string FirstName { get; init; } = null!;
    [Required] public string LastName { get; init; } = null!;
}

public record PlayerDetailsResponse
{
    [Required] public Guid Id { get; init; }
    [Required] public string FirstName { get; init; } = null!;
    [Required] public string LastName { get; init; } = null!;
    [Required] public string Country { get; init; } = null!;

    public Guid? TeamId { get; init; }
    public string? TeamName { get; init; }
    //potentially add data later
}