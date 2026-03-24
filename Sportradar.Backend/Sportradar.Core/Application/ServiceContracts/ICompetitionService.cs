using Sportradar.Core.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sportradar.Core.Application.ServiceContracts;

public interface ICompetitionService
{
    Task<CompetitionResponse?> GetCompetitionDetails(Guid competitionId);
    Task<List<CompetitionResponse>> GetAllCompetitions();
    Task<List<CompetitionResponse>> GetCompetitionsBySport(Guid sportId);
}
