using WebApplicationApi.Entity.Common;

namespace WebApplicationApi.Entity;

public class Event:BaseEntity
{
    public string Title { get; set; } = null!;
    
    public string Description { get; set; } = null!;
    
    public DateTime Date { get; set; }
    
    public string Location { get; set; } = null!;
    
    public string BannerImageUrl { get; set; } 
    
}