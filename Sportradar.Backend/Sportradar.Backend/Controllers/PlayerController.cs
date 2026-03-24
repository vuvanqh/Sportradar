using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sportradar.Core.Application.DTOs;
using Sportradar.Core.Application.ServiceContracts;

namespace Sportradar.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
/// <summary>
/// Controller for managing player-related operations such as retrieving players
/// and accessing detailed player information.
/// </summary>
public class PlayerController : ControllerBase
{
    private readonly IPlayerService _playerService;
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerController"/> class.
    /// </summary>
    /// <param name="playerService">Service responsible for handling player operations.</param>
    public PlayerController(IPlayerService playerService)   
    {
        _playerService = playerService;
    }

    /// <summary>
    /// Retrieves all players.
    /// </summary>
    /// <remarks>
    /// Returns a collection of all players available in the system.
    /// </remarks>
    /// <returns>A list of players.</returns>
    /// <response code="200">Players retrieved successfully.</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<PlayerPreviewDTO>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllPlayers()
    {
        // Placeholder for actual implementation
        return Ok(await _playerService.GetAllPlayers());
    }

    /// <summary>
    /// Retrieves detailed information about a specific player.
    /// </summary>
    /// <param name="id">The unique identifier of the player.</param>
    /// <returns>The requested player details.</returns>
    /// <response code="200">Player retrieved successfully.</response>
    /// <response code="404">Player not found.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PlayerDetailsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPlayerById(Guid id)
    {
        var resp = await _playerService.GetPlayerDetails(id);
        if (resp == null) return NotFound();
        return Ok(resp);
    }
}