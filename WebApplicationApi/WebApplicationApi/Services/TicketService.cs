using AutoMapper;
using WebApplicationApi.Dtos.TicketDtos;
using WebApplicationApi.Entity;
using WebApplicationApi.Repositories;

namespace WebApplicationApi.Services;

public class TicketService:ITicketService
{
    private readonly IRepository<Ticket> _ticketRepository;
    private readonly IMapper _mapper;

    public TicketService(IRepository<Ticket> ticketRepository, IMapper mapper)
    {
        _ticketRepository = ticketRepository;
        _mapper = mapper;
    }
    public async Task<List<TicketReturnDto>> GetAllAsync()
    {
        var tickets = await _ticketRepository.GetAllAsync();
        return _mapper.Map<List<TicketReturnDto>>(tickets);
    }

    public async Task<TicketReturnDto?> GetByIdAsync(int id)
    {
        var ticket = await _ticketRepository.GetByIdAsync(id);
        return ticket == null ? null : _mapper.Map<TicketReturnDto>(ticket);
    }

    public async Task CreateAsync(TicketCreateDto dto)
    {
        var ticket = _mapper.Map<Ticket>(dto);
        await _ticketRepository.AddAsync(ticket);
        await _ticketRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, TicketUpdateDto dto)
    {
        var ticket = await _ticketRepository.GetByIdAsync(id);
        if (ticket == null) throw new Exception("Ticket not found");
        _mapper.Map(dto, ticket);
        _ticketRepository.Update(ticket);
         await _ticketRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var ticket = await _ticketRepository.GetByIdAsync(id);
    
        if (ticket == null)
            throw new Exception("Ticket not found");

        _ticketRepository.Delete(ticket);
        await _ticketRepository.SaveChangesAsync();
    }

    public async Task<List<TicketReturnDto>> GetTicketsByEventIdAsync(int eventId)
    {
        var tickets = await _ticketRepository.GetWhereAsync(t => t.EventId == eventId);
        return _mapper.Map<List<TicketReturnDto>>(tickets);
    }
}