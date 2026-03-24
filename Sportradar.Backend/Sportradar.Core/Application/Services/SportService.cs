using Sportradar.Core.Application.DTOs;
using Sportradar.Core.Application.ServiceContracts;
using Sportradar.Core.Domain.RepositoryContracts;
using Sportradar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sportradar.Core.Application.Services;

public class SportService : ISportService
{
    private readonly ISportRepository _sportRepo;
    public SportService(ISportRepository context)
    {
        _sportRepo = context;
    }
    public async Task<List<SportResponse>> GetAllSports()
    {
        List<Sport> sports = await _sportRepo.GetAllAsync();

        return sports.Select(s => new SportResponse()
        {
            SportId = s.Id,
            Name = s.Name
        }).ToList();
    }
}
