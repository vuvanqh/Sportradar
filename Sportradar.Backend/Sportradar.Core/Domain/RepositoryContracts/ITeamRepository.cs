using Sportradar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sportradar.Core.Domain.RepositoryContracts;

public interface ITeamRepository
{
    Task<SportTeam?> GetByIdAsync(Guid teamId);
    Task<List<SportTeam>> GetAllAsync();
    Task<List<SportTeam>> GetBySportAsync(Guid sportId);
    Task<bool> ExistsAsync(Guid teamId);
}
