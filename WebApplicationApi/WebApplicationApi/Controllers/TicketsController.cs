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
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var ticket = await _ticketService.GetByIdAsync(id);
        
        if (ticket is null)
            return NotFound(ResponseModelHelper.CreateErrorResponse<string>(new List<string> { "Ticket not found." }));

        return Ok(ResponseModelHelper.CreateSuccessResponse(ticket));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] TicketUpdateDto dto)
    {
        await _ticketService.UpdateAsync(id, dto);
        
        return Ok(ResponseModelHelper.CreateSuccessResponse("Ticket updated successfully."));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _ticketService.DeleteAsync(id);
        
        return Ok(ResponseModelHelper.CreateSuccessResponse("Ticket deleted successfully."));
    }
}