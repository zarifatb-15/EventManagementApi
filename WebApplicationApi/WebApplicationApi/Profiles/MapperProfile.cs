using AutoMapper;
using WebApplicationApi.Entity;
using WebApplicationApi.Dtos.EventDtos;
using WebApplicationApi.Dtos.OrganizerDtos;
using WebApplicationApi.Dtos.TicketDtos;
namespace WebApplicationApi.Profiles;

public class MapperProfile:Profile
{
    public MapperProfile()
    {
        CreateMap<Organizer, OrganizerReturnDto>();
        CreateMap<OrganizerCreateDto, Organizer>();
        
        CreateMap<Event, EventReturnDto>();
        CreateMap<EventCreateDto, Event>();

        CreateMap<Ticket, TicketReturnDto>();
        CreateMap<TicketCreateDto, Ticket>();
    }
}