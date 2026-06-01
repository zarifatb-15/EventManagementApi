namespace WebApplicationApi.Dtos.EventDtos;

public class EventUpdateDto
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime Date { get; set; }
    public string Location { get; set; } = null!;
    public int OrganizerId { get; set; }
}