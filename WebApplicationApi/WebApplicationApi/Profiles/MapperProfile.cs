using AutoMapper;
using WebApplicationApi.Entity;
using WebApplicationApi.Dtos.EventDtos;
using WebApplicationApi.Dtos.OrganizerDtos;
using WebApplicationApi.Dtos.TicketDtos;
using WebApplicationApi.Dtos.UserDtos;

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
        
        
        CreateMap<RegisterDto, AppUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));
    }
}