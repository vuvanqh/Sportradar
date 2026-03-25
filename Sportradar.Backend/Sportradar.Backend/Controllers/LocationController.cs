using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sportradar.Core.Application.DTOs;
using Sportradar.Core.Application.ServiceContracts;

namespace Sportradar.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
/// <summary>
/// Controller for managing location-related operations such as retrieving locations,
/// countries, cities, and venues.
/// </summary>
public class LocationController : ControllerBase
{
    private readonly ILocationService _locationService;
    /// <summary>
    /// Initializes a new instance of the <see cref="LocationController"/> class.
    /// </summary>
    /// <param name="locationService">Service responsible for handling location operations.</param>
    public LocationController(ILocationService locationService)
    {
        _locationService = locationService;
    }

    /// <summary>
    /// Retrieves all locations.
    /// </summary>
    /// <returns>A list of locations.</returns>
    /// <response code="200">Locations retrieved successfully.</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<LocationDTO>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllLocations()
    {
        var resp = await _locationService.GetAllLocations();
        return Ok(resp);
    }

    /// <summary>
    /// Retrieves all available countries.
    /// </summary>
    /// <returns>A list of countries.</returns>
    /// <response code="200">Countries retrieved successfully.</response>
    [HttpGet("countries")]
    [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCountries()
    {
        var resp = await _locationService.GetAllCountries();
        return Ok(resp);
    }

    /// <summary>
    /// Retrieves all cities within a specific country.
    /// </summary>
    /// <param name="country">The name of the country.</param>
    /// <returns>A list of cities in the given country.</returns>
    /// <response code="200">Cities retrieved successfully.</response>
    [HttpGet("countries/{country}")]
    [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCities(string country)
    {
        var resp = await _locationService.GetCitiesByCountry(country);
        return Ok(resp);
    }

    /// <summary>
    /// Retrieves all venues within a specific country and city.
    /// </summary>
    /// <param name="country">The name of the country.</param>
    /// <param name="city">The name of the city.</param>
    /// <returns>A list of venues in the given location.</returns>
    /// <response code="200">Venues retrieved successfully.</response>
    [HttpGet("countries/{country}/{city}")]
    [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetVenues(string country, string city)
    {
        var resp = await _locationService.GetVenuesByLocatoin(country, city);
        return Ok(resp);
    }

    /// <summary>
    /// Retrieves detailed information about a specific location.
    /// </summary>
    /// <param name="id">The unique identifier of the location.</param>
    /// <returns>The requested location details.</returns>
    /// <response code="200">Location retrieved successfully.</response>
    /// <response code="404">Location not found.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(LocationDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetLocationDetails(Guid id)
    {
        var resp = await _locationService.GetLocationDetails(id);
        if (resp == null) return NotFound();
        return Ok(resp);
    }
}
