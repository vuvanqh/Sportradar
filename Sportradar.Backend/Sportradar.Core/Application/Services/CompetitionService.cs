using Sportradar.Core.Application.DTOs;
using Sportradar.Core.Application.ServiceContracts;
using Sportradar.Core.Domain.RepositoryContracts;
using Sportradar.Core.Entities;

namespace Sportradar.Core.Application.Services;

public class CompetitionService : ICompetitionService
{
    private readonly ICompetitionRepository _competitionRepo;
    public CompetitionService(ICompetitionRepository context)
    {
        _competitionRepo = context;
    }
    public async Task<CompetitionResponse?> GetCompetitionDetails(Guid competitionId)
    {
        var resp = await _competitionRepo.GetByIdAsync(competitionId);
        if (resp == null) return null;
        return new CompetitionResponse 
        {
            CompetitionId = resp.Id,
            Name = resp.Name,
            SportName = resp.Sport.Name
        };
    }

    public async Task<List<CompetitionResponse>> GetAllCompetitions()
    {
        var resp = await _competitionRepo.GetAllAsync();
        return resp.Select(resp => new CompetitionResponse
        {
            CompetitionId = resp.Id,
            Name = resp.Name,
            SportName = resp.Sport.Name
        }).ToList();
    }

    public async Task<List<CompetitionResponse>> GetCompetitionsBySport(Guid sportId)
    {
        var resp = await _competitionRepo.GetBySportIdAsync(sportId);
        return resp.Select(resp => new CompetitionResponse
        {
            CompetitionId = resp.Id,
            Name = resp.Name,
            SportName = resp.Sport.Name
        }).ToList();
    }
}
