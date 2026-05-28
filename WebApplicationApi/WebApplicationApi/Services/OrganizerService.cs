using AutoMapper;
using WebApplicationApi.Dtos.OrganizerDtos;
using WebApplicationApi.Entity;
using WebApplicationApi.Extensions;
using WebApplicationApi.Repositories;

namespace WebApplicationApi.Services;

public class OrganizerService:IOrganizerService
{
    private readonly IRepository<Organizer> _repository;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _env;
    public OrganizerService (IRepository<Organizer> repository, IMapper mapper, IWebHostEnvironment env)
    {
        _repository = repository;
        _mapper = mapper;
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
}