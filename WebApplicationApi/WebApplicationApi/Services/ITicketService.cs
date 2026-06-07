using WebApplicationApi.Dtos.TicketDtos;

namespace WebApplicationApi.Services;

public interface ITicketService
{
    Task<List<TicketReturnDto>> GetAllAsync();
    Task<TicketReturnDto?> GetByIdAsync(int id);
    Task CreateAsync(TicketCreateDto dto);
    Task UpdateAsync(int id, TicketUpdateDto dto);
    Task DeleteAsync(int id);
    Task<List<TicketReturnDto>> GetTicketsByEventIdAsync(int eventId);
}