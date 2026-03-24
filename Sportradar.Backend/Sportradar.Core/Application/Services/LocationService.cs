using Sportradar.Core.Application.DTOs;
using Sportradar.Core.Application.ServiceContracts;
using Sportradar.Core.Domain;
using Sportradar.Core.Domain.RepositoryContracts;
using Sportradar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sportradar.Core.Application.Services;

public class LocationService : ILocationService
{
    private readonly ILocationRepository _locationRepository;
    public LocationService(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }
    public async Task<List<string>> GetAllCountries()
    {
        var resp = await _locationRepository.GetAll();
        return resp.Select(l => l.Country).Distinct().ToList();
    }

    public async Task<List<LocationDTO>> GetAllLocations()
    {
        var resp = await _locationRepository.GetAll();
        return resp.Select(l => new LocationDTO()
        {
            LocationId = l.Id,
            City = l.City,
            Country = l.Country,
            Venue = l.Venue
        }).ToList();
    }

    public async Task<List<string>> GetCitiesByCountry(string country)
    {
        var resp = await _locationRepository.GetAll();
        return resp.Where(l=>l.Country==country).Select(l => l.City).Distinct().ToList();
    }

    public async Task<LocationDTO?> GetLocationDetails(Guid locationId)
    {
        Location? resp = await _locationRepository.GetByIdAsync(locationId);
        if (resp == null) return null;
        return new LocationDTO()
        {
            LocationId = resp.Id,
            Country = resp.Country,
            City = resp.City,
            Venue = resp.Venue
        };
    }

    public async Task<List<string?>> GetVenuesByLocatoin(string country, string city)
    {
        var resp = await _locationRepository.GetAll();
        return resp.Where(l => l.Country == country && l.City==city)
            .Select(l => l.Venue).Distinct().ToList()?? new List<string?>();
    }
}
