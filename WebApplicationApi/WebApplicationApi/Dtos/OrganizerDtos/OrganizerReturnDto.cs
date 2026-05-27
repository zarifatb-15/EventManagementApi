namespace WebApplicationApi.Dtos.OrganizerDtos;

public class OrganizerReturnDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public string? LogoUrl { get; set; }
}