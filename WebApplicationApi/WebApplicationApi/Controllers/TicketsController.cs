using Microsoft.AspNetCore.Mvc;
using WebApplicationApi.Dtos.TicketDtos;
using WebApplicationApi.Services;
using WebApplicationApi.Helpers; 

namespace WebApplicationApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketsController : ControllerBase
{
    private readonly ITicketService _ticketService;

    public TicketsController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tickets = await _ticketService.GetAllAsync();
        return Ok(ResponseModelHelper.CreateSuccessResponse(tickets));
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TicketCreateDto dto)
    {
        await _ticketService.CreateAsync(dto);
        return StatusCode(StatusCodes.Status201Created, ResponseModelHelper.CreateSuccessResponse("Ticket created successfully."));
    }
}