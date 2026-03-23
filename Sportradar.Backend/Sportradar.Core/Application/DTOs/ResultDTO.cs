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
    [Required] public string HomePlayerName { get; init; } = null!;
    [Required] public string HomePlayerSurame { get; init; } = null!;
    [Required] public string AwayPlayerName { get; init; } = null!;
    [Required] public string AwayPlayerSurame { get; init; } = null!;
    [Required] public int HomePlayerScore { get; init; }
    [Required] public int AwayPlayerScore { get; init; }
}

public record TeamResultDTO : ResultDTO
{
    [Required] public string HomeTeamName { get; init; } = null!;
    [Required] public string HomeTeamSurame { get; init; } = null!;
    [Required] public string AwayTeamName { get; init; } = null!;
    [Required] public string AwayTeamSurame { get; init; } = null!;
    [Required] public int HomeTeamScore { get; init; }
    [Required] public int AwayTeamScore { get; init; }
}

public record FreeForAllResultDTO : ResultDTO
{
    [Required] public List<FreeForAllResultEntryDTO> Results = new();
    [Required] public int NumberOfParticipants { get; init; }
}

public record FreeForAllResultEntryDTO
{
    [Required] public string PlayerName { get; init; } = null!;
    [Required] public string PlayerSurname { get; init; } = null!;
    [Required] public int Score { get; init; }
}