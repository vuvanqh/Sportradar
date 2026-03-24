using Sportradar.Core.Application.DTOs;
using Sportradar.Core.Application.ServiceContracts;
using Sportradar.Core.Domain;
using Sportradar.Core.Domain.RepositoryContracts;
using Sportradar.Core.Entities;


namespace Sportradar.Core.Application.Services;

public class TeamService : ITeamService
{
    private readonly ITeamRepository _teamRepository;
    private readonly IPlayerRepository _playerRepository;
    
    public TeamService(ITeamRepository teamRepository, IPlayerRepository playerRepository)
    {
        _teamRepository = teamRepository;
        _playerRepository = playerRepository;
    }

    public async Task<List<TeamResponse>> GetAllTeams()
    {
        return (await _teamRepository.GetAllAsync()).Select(t => new TeamResponse()
        {
            TeamId = t.Id,
            TeamName = t.Name,
            OfficialName = t.OfficialName,
            Abbreviation = t.Abbreviation,
            SportId = t.SportId
        }).ToList();
    }

    public async Task<TeamResponse?> GetTeamById(Guid teamId)
    {
        var team =  await _teamRepository.GetByIdAsync(teamId);
        if (team == null)
            return null;
        return new TeamResponse()
        {
            TeamId = team.Id,
            TeamName = team.Name,
            OfficialName = team.OfficialName,
            Abbreviation = team.Abbreviation,
            SportId = team.SportId
        };
    }

    public async Task<List<TeamResponse>> GetTeamBySportId(Guid sportId)
    {
        return (await _teamRepository.GetBySportAsync(sportId)).Select(t => new TeamResponse()
        {
            TeamId = t.Id,
            TeamName = t.Name,
            OfficialName = t.OfficialName,
            Abbreviation = t.Abbreviation,
            SportId = t.SportId
        }).ToList();
    }
    public async Task<List<PlayerPreviewDTO>> GetTeamPlayers(Guid teamId)
    {
        if (teamId == Guid.Empty || !(await _teamRepository.ExistsAsync(teamId)))
            throw new ArgumentException("Invalid team");

        var players = await _playerRepository.GetByTeamAsync(teamId);

        return players.Select(p => new PlayerPreviewDTO
        {
            PlayerId = p.Id,
            FirstName = p.FirstName,
            LastName = p.LastName
        }).ToList();
    }
}
