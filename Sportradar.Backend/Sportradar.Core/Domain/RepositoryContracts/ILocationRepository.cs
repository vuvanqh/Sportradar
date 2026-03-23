using Sportradar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sportradar.Core.Domain.RepositoryContracts;

public interface ILocationRepository
{
    Task<Location?> GetByIdAsync(Guid id);
    Task CreateAsync(Location location);
    Task DeleteAsync(Guid id);
    Task<List<Location>> GetAll();
}
