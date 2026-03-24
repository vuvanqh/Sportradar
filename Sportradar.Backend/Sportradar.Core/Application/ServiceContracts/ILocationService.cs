using Sportradar.Core.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sportradar.Core.Application.ServiceContracts;

public interface ILocationService
{
    Task<LocationDTO?> GetLocationDetails(Guid locationId);
    Task<List<LocationDTO>> GetAllLocations();
    Task<List<string>> GetAllCountries();
    Task<List<string>> GetCitiesByCountry(string country);
    Task<List<string?>> GetVenuesByLocatoin(string country, string city);
}

