using Sportradar.Core.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sportradar.Core.Application.ServiceContracts;

public interface IPlayerService
{
    Task<PlayerDetailsResponse?> GetPlayerDetails(Guid playerId);
    Task<List<PlayerPreviewDTO>> GetAllPlayers();
}
