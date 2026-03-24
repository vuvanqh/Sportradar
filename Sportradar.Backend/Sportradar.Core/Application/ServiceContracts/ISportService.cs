using Sportradar.Core.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sportradar.Core.Application.ServiceContracts;

public interface ISportService
{
    Task<List<SportResponse>> GetAllSports();
    //Task AddSport();
    //Task DeleteSport();
}
