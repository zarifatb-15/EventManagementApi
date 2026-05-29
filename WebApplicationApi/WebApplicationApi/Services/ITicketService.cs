using WebApplicationApi.Dtos.TicketDtos;

namespace WebApplicationApi.Services;

public interface ITicketService
{
    Task<List<TicketReturnDto>> GetAllAsync();
    Task<TicketReturnDto?> GetByIdAsync(int id);
    Task CreateAsync(TicketCreateDto dto);
    Task<List<TicketReturnDto>> GetTicketsByEventIdAsync(int eventId);
}