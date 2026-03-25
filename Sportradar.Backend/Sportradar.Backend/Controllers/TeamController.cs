using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sportradar.Core.Application.DTOs;
using Sportradar.Core.Application.ServiceContracts;
using System.Threading.Tasks;

namespace Sportradar.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
/// <summary>
/// API controller responsible for handling operations related to teams.
/// Provides endpoints for retrieving teams and their associated players.
/// </summary>
public class TeamController : ControllerBase
{
    private readonly ITeamService _teamService;
    /// <summary>
    /// Initializes a new instance of the <see cref="TeamController"/> class.
    /// </summary>
    /// <param name="teamService">Service responsible for handling team operations.</param>
    public TeamController(ITeamService teamService)
    {
        _teamService = teamService;
    }

    /// <summary>
    /// Retrieves all teams.
    /// </summary>
    /// <remarks>
    /// Returns a collection of teams available in the system.
    /// </remarks>
    /// <returns>A list of teams.</returns>
    /// <response code="200">Teams retrieved successfully.</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<TeamResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllTeams()
    {
        var resp = await _teamService.GetAllTeams();
        return Ok(resp);
    }

    /// <summary>
    /// Retrieves a specific team by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the team.</param>
    /// <returns>The requested team.</returns>
    /// <response code="200">Team retrieved successfully.</response>
    /// <response code="404">Team not found.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TeamResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTeamById(Guid id)
    {
        var resp = await _teamService.GetTeamById(id);
        return Ok(resp);
    }

    /// <summary>
    /// Retrieves all teams associated with a specific sport.
    /// </summary>
    /// <remarks>
    /// Returns a list of teams that belong to the given sport identifier.
    /// </remarks>
    /// <param name="id">The unique identifier of the sport.</param>
    /// <returns>A list of teams associated with the sport.</returns>
    /// <response code="200">Teams retrieved successfully.</response>
    [HttpGet("sport/{id}")]
    [ProducesResponseType(typeof(List<TeamResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTeamsBySportId(Guid id)
    {
        var resp = await _teamService.GetTeamBySportId(id);
        return Ok(resp);
    }

    /// <summary>
    /// Retrieves all players belonging to a specific team.
    /// </summary>
    /// <remarks>
    /// Returns all players assigned to the given team.
    /// </remarks>
    /// <param name="id">The unique identifier of the team.</param>
    /// <returns>A list of players in the team.</returns>
    /// <response code="200">Players retrieved successfully.</response>
    /// <response code="404">Team not found.</response>
    [HttpGet("{id}/players")]
    [ProducesResponseType(typeof(List<PlayerPreviewDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPlayersByTeamId(Guid id)
    {
        var resp = await _teamService.GetTeamPlayers(id);
        return Ok(resp);
    }
}
