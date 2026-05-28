using Microsoft.AspNetCore.Mvc;
using WebApplicationApi.Attributes;
using WebApplicationApi.Dtos.OrganizerDtos;
using WebApplicationApi.Services;

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
        return Ok(organizers);
    }
    
    [HttpGet("{id}")]
    
    public async Task<IActionResult> GetById(int id)
    {
        var organizer = await _organizerService.GetByIdAsync(id);
        if (organizer == null) return NotFound();
        return Ok(organizer);
    }
    
    [HttpPost]
    
    public async Task<IActionResult> Create([FromBody] OrganizerCreateDto dto)
    {
        await _organizerService.CreateAsync(dto);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPost("{id}/logo")]
    public async Task<IActionResult> UploadLogo(
        [FromRoute]int id,
        [FileTypes("image/jpeg", "image/png")] [FileLength(2)]
        IFormFile? file)
    {
        if(file==null || file.Length == 0) return BadRequest("File is not selected");
        
        await _organizerService.UploadLogoAsync(id, file);
        return Ok();
    }
    
}