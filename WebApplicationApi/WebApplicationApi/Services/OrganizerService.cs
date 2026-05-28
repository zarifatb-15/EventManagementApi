using AutoMapper;
using WebApplicationApi.Dtos.OrganizerDtos;
using WebApplicationApi.Entity;
using WebApplicationApi.Repositories;

namespace WebApplicationApi.Services;

public class OrganizerService:IOrganizerService
{
    private readonly IRepository<Organizer> _repository;
    private readonly IMapper _mapper;
    public OrganizerService (IRepository<Organizer> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
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
}