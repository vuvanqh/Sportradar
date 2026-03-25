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
/// Controller for managing competition-related operations such as retrieving competitions
/// and filtering them by sport.
/// </summary>
public class CompetitionController : ControllerBase
{
    private readonly ICompetitionService _competitionService;
    /// <summary>
    /// Initializes a new instance of the <see cref="CompetitionController"/> class.
    /// </summary>
    /// <param name="competitionService">Service responsible for handling competition operations.</param>
    public CompetitionController(ICompetitionService competitionService)
    {
        _competitionService = competitionService;
    }

    /// <summary>
    /// Retrieves all competitions.
    /// </summary>
    /// <remarks>
    /// Returns a list of all competitions along with their associated sport names.
    /// </remarks>
    /// <returns>A list of competitions.</returns>
    /// <response code="200">Competitions retrieved successfully.</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<CompetitionResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCompetitions()
    {
        return Ok(await _competitionService.GetAllCompetitions());
    }

    /// <summary>
    /// Retrieves all competitions for a specific sport.
    /// </summary>
    /// <param name="sportId">The unique identifier of the sport.</param>
    /// <returns>A list of competitions associated with the given sport.</returns>
    /// <response code="200">Competitions retrieved successfully.</response>
    [HttpGet("{sportId:guid}")]
    [ProducesResponseType(typeof(List<CompetitionResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCompetitionsBySportId(Guid sportId)
    {
        return Ok(await _competitionService.GetCompetitionsBySport(sportId));
    }
}
