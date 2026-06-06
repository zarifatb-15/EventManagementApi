using AutoMapper;
using WebApplicationApi.Dtos.EventDtos;
using WebApplicationApi.Dtos.OrganizerDtos;
using WebApplicationApi.Entity;
using WebApplicationApi.Extensions;
using WebApplicationApi.Repositories;

namespace WebApplicationApi.Services;

public class OrganizerService:IOrganizerService
{
    private readonly IRepository<Organizer> _repository;
    private readonly IMapper _mapper;
    private readonly IRepository<Event> _eventRepository;
    private readonly IWebHostEnvironment _env;
    public OrganizerService (IRepository<Organizer> repository, IMapper mapper,IRepository<Event> eventRepository, IWebHostEnvironment env)
    {
        _repository = repository;
        _mapper = mapper;
        _eventRepository = eventRepository;
        _env = env;
    }
    public async Task<List<OrganizerReturnDto>> GetAllAsync()
    {
       var organizers = await _repository.GetAllAsync();
       return _mapper.Map<List<OrganizerReturnDto>>(organizers);
    }

    public async Task<OrganizerReturnDto?> GetByIdAsync(int id)
    {
        var organizer = await _repository.GetByIdAsync(id); 
        if (organizer == null) return null;
        return _mapper.Map<OrganizerReturnDto>(organizer);
    }

    public async Task DeleteAsync(int id)
    {
        var existingOrganizer = await _repository.GetByIdAsync(id);
        if (existingOrganizer == null)  throw new Exception("Organizer not found");
         _repository.Delete( existingOrganizer);
         await _repository.SaveChangesAsync();
    }

    public async Task<List<EventReturnDto>> GetEventsByOrganizerIdAsync(int organizerId)
    {
        var events = await _eventRepository.GetWhereAsync(e => e.OrganizerId == organizerId);
        return _mapper.Map<List<EventReturnDto>>(events);
    }

    public async Task CreateAsync(OrganizerCreateDto dto)
    {
      var organizer = _mapper.Map<Organizer>(dto);
      await _repository.AddAsync(organizer);
      await _repository.SaveChangesAsync();
    }

    public async Task UploadLogoAsync(int id, IFormFile file)
    {
        var organizer = await _repository.GetByIdAsync(id);
        if (organizer == null) throw new Exception("Organizer not found");
        
        var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "organizers");
        if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
        
        var savedFileName =await file.SaveFileAsync(uploadsFolder);
        
        organizer.LogoUrl = $"/uploads/organizers/{savedFileName}";
        _repository.Update(organizer);
        await _repository.SaveChangesAsync();
        
    }

    public async Task UpdateAsync(int id, OrganizerUpdateDto dto)
    {
        var existingOrganizer = await _repository.GetByIdAsync(id); 
        if (existingOrganizer == null) 
            throw new Exception("Organizer not found"); 
        
        _mapper.Map(dto, existingOrganizer);
        _repository.Update(existingOrganizer);
        await  _repository.SaveChangesAsync();
    }
}