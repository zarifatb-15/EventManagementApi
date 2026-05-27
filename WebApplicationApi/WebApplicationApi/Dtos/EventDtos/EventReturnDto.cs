namespace WebApplicationApi.Dtos.EventDtos;

public class EventReturnDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; } = null!;
    public string? BannerImageUrl { get; set; }
    public int OrganizerId { get; set; }
}