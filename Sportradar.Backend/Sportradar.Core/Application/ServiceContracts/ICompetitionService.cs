using System;
using System.Collections.Generic;
using System.Text;

namespace Sportradar.Core.Application.ServiceContracts;

public interface ICompetitionService
{
    Task GetCompetitionDetails(Guid competitionId);
    Task GetCompetitionParticipants(Guid competitionId);
}
