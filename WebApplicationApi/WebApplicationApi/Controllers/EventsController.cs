using Microsoft.AspNetCore.Mvc;
using WebApplicationApi.Attributes;
using WebApplicationApi.Dtos.EventDtos;
using WebApplicationApi.Dtos.TicketDtos;
using WebApplicationApi.Services;

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
        return Ok(events);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EventCreateDto dto)
    {
        await _eventService.CreateAsync(dto);
        return StatusCode(StatusCodes.Status201Created);
    }
    
    [HttpPost("{id}/banner")]
    public async Task<IActionResult> UploadBanner(
        [FromRoute] int id, 
        [FileTypes("image/jpeg", "image/png")] [FileLength(2)] IFormFile? file)
    {
        if (file == null || file.Length == 0) return BadRequest("File is not selected");
        
        await _eventService.UploadBannerAsync(id, file);
        return Ok();
    }
    [HttpGet("{eventId}/tickets")]
    public async Task<IActionResult> GetTickets(int eventId)
    {
        var tickets = await _ticketService.GetTicketsByEventIdAsync(eventId);
        return Ok(tickets);
    }
    
    [HttpGet("{eventId}/organizer")]
    public async Task<IActionResult> GetOrganizer(int eventId)
    {
        var organizer = await _eventService.GetOrganizerByEventIdAsync(eventId);
        if (organizer == null) return NotFound();
        return Ok(organizer);
    }
    
    [HttpPost("{eventId}/tickets")]
    public async Task<IActionResult> CreateTicketForEvent(int eventId, [FromBody] TicketCreateDto dto)
    {
        dto.EventId = eventId; 
        await _ticketService.CreateAsync(dto);
        return StatusCode(StatusCodes.Status201Created);
    }
    
    
}