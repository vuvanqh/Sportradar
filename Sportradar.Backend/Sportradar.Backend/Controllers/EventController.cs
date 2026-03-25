using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sportradar.Core.Application.DTOs;
using Sportradar.Core.Application.ServiceContracts;

namespace Sportradar.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
/// <summary>
/// Controller for managing event-related operations such as retrieving, creating, updating, and deleting events,
/// as well as managing event participants.
/// </summary>
public class EventController : ControllerBase
{
    private readonly IEventService _eventService;
    /// <summary>
    /// Initializes a new instance of the <see cref="EventController"/> class.
    /// </summary>
    /// <param name="eventService">Service for handling event operations.</param>
    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }

    /// <summary>
    /// Retrieves all events.
    /// </summary>
    /// <remarks>
    /// Returns a polymorphic list of events.  
    /// Each item can be one of:
    /// - <see cref="TeamEventResponse"/>
    /// - <see cref="OneOnOneEventResponse"/>
    /// - <see cref="FreeForAllEventResponse"/>
    /// 
    /// The discriminator field <c>eventType</c> determines the concrete type.
    /// </remarks>
    /// <returns>A list of events.</returns>
    /// <response code="200">Events retrieved successfully.</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<EventResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var resp = await _eventService.GetAllEvents();
        return Ok(resp);
    }

    /// <summary>
    /// Deletes an event by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the event to delete.</param>
    /// <returns>No content if deletion is successful.</returns>
    /// <response code="204">Event deleted successfully.</response>
    /// <response code="400">Invalid request or event could not be deleted.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _eventService.DeleteEvent(id);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        return NoContent();
    }


    /// <summary>
    /// Creates a new free-for-all event.
    /// </summary>
    /// <param name="request">FreeForAll event creation data.</param>
    /// <response code="200">Event created successfully.</response>
    /// <response code="400">
    /// Invalid request or scheduling conflict (e.g., overlapping event at the same location).
    /// </response>
    [HttpPost("create/FreeForAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateFreeForAll(CreateFreeForAllEventRequest request)
    {             
        try
        {
            await _eventService.CreateEvent(request);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        return Ok(new {Message = "Event created Siccesfi;;y"});
    }

    /// <summary>
    /// Creates a new OneOnOne event.
    /// </summary>
    /// <param name="request">OneOnOne event creation data.</param>
    /// <response code="200">Event created successfully.</response>
    /// <response code="400">Invalid request data.</response>
    [HttpPost("create/OneOnOne")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateOneOnOne(CreateOneOnOneEventRequest request)
    {
        try
        {
            await _eventService.CreateEvent(request);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        return Ok(new { Message = "Event created Siccesfi;;y" });
    }

    /// <summary>
    /// Creates a new Team event.
    /// </summary>
    /// <param name="request">Team event creation data.</param>
    /// <response code="200">Event created successfully.</response>
    /// <response code="400">Invalid request data.</response>
    [HttpPost("create/Team")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(CreateTeamEventRequest request)
    {
        try
        {
            await _eventService.CreateEvent(request);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        return Ok(new { Message = "Event created Siccesfi;;y" });
    }


    /// <summary>
    /// Adds a participant to an event.
    /// </summary>
    /// <remarks>
    /// Only applicable to events that support participants (e.g. FreeForAll).
    /// </remarks>
    /// <param name="id">The ID of the event.</param>
    /// <param name="participantId">The ID of the participant to add.</param>
    /// <response code="200">Participant added successfully.</response>
    /// <response code="400">Invalid request.</response>
    [HttpPatch("{id}/add-participant/{participantId}")]
    public async Task<IActionResult> AddParticipant(Guid id, Guid participantId)
    {
        try
        {
            await _eventService.AddParticipant(id, participantId);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        return Ok();
    }

    /// <summary>
    /// Removes a participant from an event.
    /// </summary>
    /// <remarks>
    /// Only applicable to events that support participants (e.g. FreeForAll).
    /// </remarks>
    /// <param name="id">The ID of the event.</param>
    /// <param name="participantId">The ID of the participant to remove.</param>
    /// <response code="200">Participant removed successfully.</response>
    /// <response code="400">Invalid request.</response>
    [HttpPatch("{id}/remove-participant/{participantId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RemoveParticipant(Guid id, Guid participantId)
    {
        try
        {
            await _eventService.RemoveParticipant(id, participantId);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        return Ok();
    }

    /// <summary>
    /// Updates an existing event.
    /// </summary>
    /// <remarks>
    /// Accepts a polymorphic request body.  
    /// The request must match one of:
    /// - <see cref="UpdateTeamEventRequest"/>
    /// - <see cref="UpdateOneOnOneEventRequest"/>
    /// 
    /// The <c>eventType</c> discriminator determines the update logic.
    /// </remarks>
    /// <param name="request">The update request.</param>
    /// <response code="200">Event updated successfully.</response>
    /// <response code="400">Invalid request data.</response>
    [HttpPatch("update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(UpdateEventRequest request)
    {
        try
        {
            await _eventService.UpdateEvent(request);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        return Ok();
    }
}
