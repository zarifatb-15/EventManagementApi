using Microsoft.AspNetCore.Mvc;
using WebApplicationApi.Attributes;
using WebApplicationApi.Dtos.OrganizerDtos;
using WebApplicationApi.Services;
using WebApplicationApi.Helpers; 

namespace WebApplicationApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrganizersController : ControllerBase
{
    private readonly IOrganizerService _organizerService;
    
    public OrganizersController(IOrganizerService organizerService)
    {
       _organizerService = organizerService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var organizers = await _organizerService.GetAllAsync();
        return Ok(ResponseModelHelper.CreateSuccessResponse(organizers));
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var organizer = await _organizerService.GetByIdAsync(id);
        if (organizer == null) 
        {
            return NotFound(ResponseModelHelper.CreateErrorResponse<string>(new List<string> { "Organizer not found." }));
        }
        
        return Ok(ResponseModelHelper.CreateSuccessResponse(organizer));
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] OrganizerCreateDto dto)
    {
        await _organizerService.CreateAsync(dto);
        return StatusCode(StatusCodes.Status201Created, ResponseModelHelper.CreateSuccessResponse("Organizer created successfully."));
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute]int id, [FromBody] OrganizerUpdateDto dto)
    {
     await _organizerService.UpdateAsync(id, dto);
    return Ok(ResponseModelHelper.CreateSuccessResponse("Organizer updated successfully."));
    }

    [HttpPost("{id}/logo")]
    public async Task<IActionResult> UploadLogo(
        [FromRoute]int id,
        [FileTypes("image/jpeg", "image/png")] [FileLength(2)]
        IFormFile? file)
    {
        if(file == null || file.Length == 0) 
        {
            return BadRequest(ResponseModelHelper.CreateErrorResponse<string>(new List<string> { "File is not selected." }));
        }
        
        await _organizerService.UploadLogoAsync(id, file);
        return Ok(ResponseModelHelper.CreateSuccessResponse("Logo uploaded successfully."));
    }
    
    [HttpGet("{organizerId}/events")]
    public async Task<IActionResult> GetEventsForOrganizer(int organizerId)
    {
        var events = await _organizerService.GetEventsByOrganizerIdAsync(organizerId);
        return Ok(ResponseModelHelper.CreateSuccessResponse(events));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _organizerService.DeleteAsync(id);
        return Ok(ResponseModelHelper.CreateSuccessResponse("Organizer deleted successfully."));
    }
}