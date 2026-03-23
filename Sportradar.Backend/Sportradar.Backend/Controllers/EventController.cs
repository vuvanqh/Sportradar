using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sportradar.Core.Application.DTOs;
using Sportradar.Core.Application.ServiceContracts;

namespace Sportradar.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly IEventService _eventService;
    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var resp = await _eventService.GetAllEvents();
        return Ok(resp);
    }

    [HttpDelete("{id}")]
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
    [HttpPost("create")]
     public async Task<IActionResult> Create(CreateEventRequest request)
    {             
        try
        {
            await _eventService.CreateEvent(request);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        return Ok();
    }
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
    [HttpPatch("{id}/remove-participant/{participantId}")]
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

    [HttpPatch("update")]
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
