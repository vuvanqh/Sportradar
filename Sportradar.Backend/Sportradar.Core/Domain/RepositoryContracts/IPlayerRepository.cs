using Sportradar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sportradar.Core.Domain.RepositoryContracts;

public interface IPlayerRepository
{
    Task<List<Player>> GetAll();
    Task<List<Player>> GetByTeamAsync(Guid teamId);
    Task<Player?> GetByIdAsync(Guid id);
    Task CreateAsync(Player loplayercation);
    Task DeleteAsync(Guid id);
}