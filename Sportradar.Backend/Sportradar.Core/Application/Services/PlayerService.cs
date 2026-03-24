using Sportradar.Core.Application.DTOs;
using Sportradar.Core.Application.ServiceContracts;
using Sportradar.Core.Domain;
using Sportradar.Core.Domain.RepositoryContracts;
using Sportradar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sportradar.Core.Application.Services;

public class PlayerService : IPlayerService
{
    private readonly IPlayerRepository _playerRepository;
    public PlayerService(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }
    public async Task<List<PlayerPreviewDTO>> GetAllPlayers()
    {
        var resp = await _playerRepository.GetAll();
        return resp.Select(p => new PlayerPreviewDTO
        {
            PlayerId = p.Id,
            FirstName = p.FirstName,
            LastName = p.LastName
        }).ToList();
    }

    public async Task<PlayerDetailsResponse?> GetPlayerDetails(Guid playerId)
    {
        Player? resp = await _playerRepository.GetByIdAsync(playerId);
        if (resp == null) return null;
        return new PlayerDetailsResponse
        {
            PlayerId = resp.Id,
            FirstName = resp.FirstName,
            LastName = resp.LastName,
            Country = resp.Country,
            TeamId = resp.TeamId,
            TeamName = resp.Team?.Name
        };
    }
}
