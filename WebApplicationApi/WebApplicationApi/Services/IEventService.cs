using WebApplicationApi.Dtos.EventDtos;
using WebApplicationApi.Dtos.OrganizerDtos;

namespace WebApplicationApi.Services;

public interface IEventService
{
    Task<List<EventReturnDto>> GetAllAsync();
    Task<EventReturnDto?> GetByIdAsync(int id);
    Task CreateAsync(EventCreateDto dto);
    Task UploadBannerAsync(int id, IFormFile file);
    Task<OrganizerReturnDto?> GetOrganizerByEventIdAsync(int eventId);
    Task UpdateAsync(int id,EventUpdateDto dto);
    Task DeleteAsync(int id);
}