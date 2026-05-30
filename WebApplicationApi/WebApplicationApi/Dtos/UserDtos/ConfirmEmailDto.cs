namespace WebApplicationApi.Dtos.UserDtos;

public class ConfirmEmailDto
{
    public string UserId { get; set; } = null!;
    public string Token { get; set; } = null!;
}