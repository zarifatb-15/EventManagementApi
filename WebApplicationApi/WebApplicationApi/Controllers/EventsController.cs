using Microsoft.AspNetCore.Mvc;
using WebApplicationApi.Attributes;
using WebApplicationApi.Dtos.EventDtos;
using WebApplicationApi.Dtos.TicketDtos;
using WebApplicationApi.Services;
using WebApplicationApi.Helpers; 

namespace WebApplicationApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;
    private readonly ITicketService _ticketService;
    
    public EventsController(IEventService eventService, ITicketService ticketService)
    {
        _eventService = eventService;
        _ticketService = ticketService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var events = await _eventService.GetAllAsync();
        return Ok(ResponseModelHelper.CreateSuccessResponse(events));
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EventCreateDto dto)
    {
        await _eventService.CreateAsync(dto);
        return StatusCode(StatusCodes.Status201Created, ResponseModelHelper.CreateSuccessResponse("Event created successfully."));
    }
    
    [HttpPut("{id}")]
    
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] EventUpdateDto dto)
    {
        await _eventService.UpdateAsync(id, dto);
        return Ok(ResponseModelHelper.CreateSuccessResponse("Event updated successfully."));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _eventService.DeleteAsync(id);
        return Ok(ResponseModelHelper.CreateSuccessResponse("Event deleted successfully."));
    }
    
    [HttpPost("{id}/banner")]
    public async Task<IActionResult> UploadBanner(
        [FromRoute] int id, 
        [FileTypes("image/jpeg", "image/png")] [FileLength(2)] IFormFile? file)
    {
        if (file == null || file.Length == 0) 
        {
            return BadRequest(ResponseModelHelper.CreateErrorResponse<string>(new List<string> { "File is not selected" }));
        }
        
        await _eventService.UploadBannerAsync(id, file);
        return Ok(ResponseModelHelper.CreateSuccessResponse("Banner uploaded successfully."));
    }

    [HttpGet("{eventId}/tickets")]
    public async Task<IActionResult> GetTickets(int eventId)
    {
        var tickets = await _ticketService.GetTicketsByEventIdAsync(eventId);
        return Ok(ResponseModelHelper.CreateSuccessResponse(tickets));
    }
    
    [HttpGet("{eventId}/organizer")]
    public async Task<IActionResult> GetOrganizer(int eventId)
    {
        var organizer = await _eventService.GetOrganizerByEventIdAsync(eventId);
        if (organizer == null) 
        {
            return NotFound(ResponseModelHelper.CreateErrorResponse<string>(new List<string> { "Organizer not found." }));
        }
        
        return Ok(ResponseModelHelper.CreateSuccessResponse(organizer));
    }
    
    [HttpPost("{eventId}/tickets")]
    public async Task<IActionResult> CreateTicketForEvent(int eventId, [FromBody] TicketCreateDto dto)
    {
        dto.EventId = eventId; 
        await _ticketService.CreateAsync(dto);
        return StatusCode(StatusCodes.Status201Created, ResponseModelHelper.CreateSuccessResponse("Ticket created successfully."));
    }
}