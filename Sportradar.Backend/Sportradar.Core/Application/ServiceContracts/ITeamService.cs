using Sportradar.Core.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sportradar.Core.Application.ServiceContracts;

public interface ITeamService
{
    Task<TeamResponse?> GetTeamById(Guid teamId);
    Task<List<TeamResponse>> GetAllTeams();
    Task<List<TeamResponse>> GetTeamBySportId(Guid sportId);
    Task<List<PlayerPreviewDTO>> GetTeamPlayers(Guid teamId);
}
