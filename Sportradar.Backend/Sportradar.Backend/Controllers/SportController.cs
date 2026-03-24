using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sportradar.Core.Application.DTOs;
using Sportradar.Core.Application.ServiceContracts;
using Sportradar.Infrastructure;

namespace Sportradar.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
/// <summary>
/// Controller for managing sport-related operations such as retrieving available sports.
/// </summary>
public class SportController : ControllerBase
{
    private readonly ISportService _sportService;
    /// <summary>
    /// Initializes a new instance of the <see cref="SportController"/> class.
    /// </summary>
    /// <param name="sportService">Service responsible for handling sport operations.</param>
    public SportController(ISportService sportService)
    {
        _sportService = sportService;
    }

    /// <summary>
    /// Retrieves all sports.
    /// </summary>
    /// <remarks>
    /// Returns a list of all available sports with their identifiers and names.
    /// </remarks>
    /// <returns>A list of sports.</returns>
    /// <response code="200">Sports retrieved successfully.</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<SportResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllSports()
    {
        return Ok(await _sportService.GetAllSports());
    }
}
