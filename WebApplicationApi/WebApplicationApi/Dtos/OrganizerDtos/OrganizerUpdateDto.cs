namespace WebApplicationApi.Dtos.OrganizerDtos;

public class OrganizerUpdateDto
{  
    public string Name { get; set; } = null!;
    public string ContactEmail { get; set; } = null!;
    public string ContactPhone { get; set; } = null!;
    public string Address { get; set; } = null!;
}