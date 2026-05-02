using WebApplicationApi.Entity.Common;

namespace WebApplicationApi.Entity;

public class Ticket:BaseEntity
{
    public EventId EventId { get; set; }

    public string Type { get; set; } = null!;
    
    public decimal Price { get; set; }
    
    public int QuantityAvailable { get; set; }
}