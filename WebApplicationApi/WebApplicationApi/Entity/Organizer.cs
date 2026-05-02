using WebApplicationApi.Entity.Common;

namespace WebApplicationApi.Entity;

public class Organizer:BaseEntity
{
    public string Name { get; set; } = null!;
    
    public string Email { get; set; } = null!;
    
    public string Phone { get; set; } = null!;
    
    public string LogoUrl { get; set; } = null!;
    
    public List<Event> Events { get; set; } 
}