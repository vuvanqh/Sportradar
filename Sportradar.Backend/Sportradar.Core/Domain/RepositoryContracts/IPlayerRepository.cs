using Sportradar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sportradar.Core.Domain.RepositoryContracts;

public interface IPlayerRepository
{
    Task<List<Player>> GetAll();
    Task<Player?> GetByIdAsync(Guid id);
    Task CreateAsync(Player loplayercation);
    Task DeleteAsync(Guid id);
}