using System;
using System.Collections.Generic;
using System.Text;

namespace Sportradar.Core.Application.ServiceContracts;

public interface ISportService
{
    Task GetAllSports();
    Task AddSport();
    Task DeleteSport();
}
