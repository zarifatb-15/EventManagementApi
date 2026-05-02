using WebApplicationApi.Entity.Common;

namespace WebApplicationApi.Entity;

public class Event:BaseEntity
{
    public string Title { get; set; } = null!;
    
    public string Description { get; set; } = null!;
    
    public DateTime Date { get; set; }
    
    public string Location { get; set; } = null!;
    
    public string BannerImageUrl { get; set; } 
   
    public int OrganizerId { get; set; }
    public Organizer Organizer { get; set; }
    
    public List<Ticket> Tickets { get; set; }
    
}