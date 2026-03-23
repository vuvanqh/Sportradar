using Sportradar.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sportradar.Core.Application.DTOs;

public record ResultDTO
{
    [Required] public Guid ResultId { get; init; } 
    [Required] public string EventTitle { get; init; } = null!;
    [Required] public ResultType ResultType { get; init; }
}

public record OneOnOneResultDTO : ResultDTO
{
    [Required] public Guid HomePlayerId { get; init; }
    [Required] public Guid AwayPlayerId { get; init; }
    [Required] public string HomePlayerFirstName { get; init; } = null!;
    [Required] public string HomePlayerLastName { get; init; } = null!;
    [Required] public string AwayPlayerFirstName { get; init; } = null!;
    [Required] public string AwayPlayerLastName { get; init; } = null!;
    [Required] public int HomePlayerScore { get; init; }
    [Required] public int AwayPlayerScore { get; init; }
}

public record TeamResultDTO : ResultDTO
{
    [Required] public Guid HomeTeamId { get; init; }
    [Required] public Guid AwayTeamId { get; init; }
    [Required] public string HomeTeamName { get; init; } = null!;
    [Required] public string AwayTeamName { get; init; } = null!;
    [Required] public int HomeTeamScore { get; init; }
    [Required] public int AwayTeamScore { get; init; }
}

public record FreeForAllResultDTO : ResultDTO
{
    [Required] public List<FreeForAllResultEntryDTO> Results { get; set; } = new();
    [Required] public int NumberOfParticipants { get; init; }
}

public record FreeForAllResultEntryDTO
{
    [Required] public Guid PlayerId { get; init; }
    [Required] public string FirstName { get; init; } = null!;
    [Required] public string LastName { get; init; } = null!;
    [Required] public int Score { get; init; }
}