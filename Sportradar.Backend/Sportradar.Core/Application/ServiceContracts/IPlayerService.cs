using System;
using System.Collections.Generic;
using System.Text;

namespace Sportradar.Core.Application.ServiceContracts;

public interface IPlayerService
{
    Task GetPlayerDetails(Guid playerId);
}
