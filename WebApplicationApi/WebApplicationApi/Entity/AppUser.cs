using Microsoft.AspNetCore.Identity;

namespace WebApplicationApi.Entity;

public class AppUser:IdentityUser
{
    public string FullName { get; set; }= null!;
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpireTime { get; set; }
}