using Sportradar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sportradar.Core.Domain.RepositoryContracts;

public interface ICompetitionRepository
{
    Task<Competition?> GetByIdAsync(Guid competitionId);
    Task<List<Competition>> GetBySportIdAsync(Guid sportId);
    Task<List<Competition>> GetAllAsync();
}
