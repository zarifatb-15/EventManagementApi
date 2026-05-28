using WebApplicationApi.Dtos.OrganizerDtos;

namespace WebApplicationApi.Services;

public interface IOrganizerService
{
    Task<List<OrganizerReturnDto>> GetAllAsync();
    Task<OrganizerReturnDto?> GetByIdAsync(int id);
    Task CreateAsync(OrganizerCreateDto dto);
}