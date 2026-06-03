using AutoMapper;
using WebApplicationApi.Dtos.EventDtos;
using WebApplicationApi.Dtos.OrganizerDtos;
using WebApplicationApi.Entity;
using WebApplicationApi.Extensions;
using WebApplicationApi.Repositories;

namespace WebApplicationApi.Services;

public class EventService:IEventService
{
    private readonly IRepository<Event> _eventRepository;
    private readonly IRepository<Organizer> _organizerRepository;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _env;
    
    public EventService(
        IRepository<Event> eventRepository, 
        IRepository<Organizer> organizerRepository, 
        IMapper mapper, 
        IWebHostEnvironment env)
    {
        _eventRepository = eventRepository;
        _organizerRepository = organizerRepository;
        _mapper = mapper;
        _env = env;
    }
    public async Task<List<EventReturnDto>> GetAllAsync()
    {
        var events = await _eventRepository.GetAllAsync();
        return _mapper.Map<List<EventReturnDto>>(events);
    }

    public async Task<EventReturnDto?> GetByIdAsync(int id)
    {
      var ev = await _eventRepository.GetByIdAsync(id);
      return ev == null ? null : _mapper.Map<EventReturnDto>(ev);
    }

    public async Task CreateAsync(EventCreateDto dto)
    {
        var ev = _mapper.Map<Event>(dto);
        await _eventRepository.AddAsync(ev);
        await _eventRepository.SaveChangesAsync();
    }

    public async Task UploadBannerAsync(int id, IFormFile file)
    {
        var ev = await _eventRepository.GetByIdAsync(id);
        if (ev == null) throw new Exception("Event not found");

        var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "events");
        if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

        var savedFileName = await file.SaveFileAsync(uploadsFolder);
        ev.BannerImageUrl = $"/uploads/events/{savedFileName}";

        _eventRepository.Update(ev);
        await _eventRepository.SaveChangesAsync();
    }

    public async Task<OrganizerReturnDto?> GetOrganizerByEventIdAsync(int eventId)
    {
        var ev = await _eventRepository.GetByIdAsync(eventId);
        if (ev == null) return null;

        var organizer = await _organizerRepository.GetByIdAsync(ev.OrganizerId);
        return _mapper.Map<OrganizerReturnDto>(organizer);
    }

    public async Task UpdateAsync(int id, EventUpdateDto dto)
    {
        var existingEvent = await _eventRepository.GetByIdAsync(id); 
        if (existingEvent == null) 
            throw new Exception("Event not found"); 
        _mapper.Map(dto, existingEvent);
        
        _eventRepository.Update(existingEvent);
        await _eventRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var existingEvent = await _eventRepository.GetByIdAsync(id);
        if (existingEvent == null) 
            throw new Exception("Event not found");
        
        _eventRepository.Delete(existingEvent);
        
        await _eventRepository.SaveChangesAsync();
       
    }
}