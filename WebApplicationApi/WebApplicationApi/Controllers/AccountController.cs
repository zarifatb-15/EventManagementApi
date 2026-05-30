using System.Security.Claims;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplicationApi.Dtos.UserDtos;
using WebApplicationApi.Entity;
using WebApplicationApi.Services;

namespace WebApplicationApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController (
    IValidator<RegisterDto> registerValidator,
    UserManager<AppUser> userManager,
    RoleManager<IdentityRole> roleManager,
    IConfiguration config,
    JwtService jwtService,
    IMapper mapper
    ): ControllerBase
{

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var validationResult = registerValidator.Validate(registerDto);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var user = await userManager.FindByNameAsync(registerDto.UserName); 
        if (user is not null)
            return BadRequest("User with this username already exists");

        user = mapper.Map<AppUser>(registerDto);

        var result = await userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
            return BadRequest(result.Errors);
        
        await userManager.AddToRoleAsync(user, "Member");
        
        // Email təsdiqi üçün token yaradılır
        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        
        return Ok(new 
        { 
            message = "User created successfully. Please confirm your email.",  
            userId = user.Id, 
            token = token 
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = await userManager.FindByNameAsync(loginDto.UserName); 
        if (user is null)
            return BadRequest("Invalid username or password");

        var result = await userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!result)
            return BadRequest("Invalid username or password");
        
        if (!await userManager.IsEmailConfirmedAsync(user))
            return BadRequest("Please confirm your email before logging in.");

        var roles = await userManager.GetRolesAsync(user);

        return Ok(new
        {
            token = jwtService.GenerateToken(user, roles, config)
        });
    }

    [HttpGet("profile")]
    [Authorize]
    public IActionResult Profile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userName = User.Identity?.Name ?? User.FindFirstValue(ClaimTypes.Name);
        var fullName = User.FindFirstValue("FullName");
        var roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();

        return Ok(new
        {
            userId,
            fullName,
            userName,
            roles
        });
    }
    
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is null)
            return BadRequest("User with this email does not exist.");
        
        var token = await userManager.GeneratePasswordResetTokenAsync(user);

        return Ok(new { message = "Password reset token generated.", token = token });
    }
    
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
    {
        var user = await userManager.FindByEmailAsync(resetPasswordDto.Email);
        if (user is null)
            return BadRequest("User with this email does not exist.");

        var result = await userManager.ResetPasswordAsync(
            user, 
            resetPasswordDto.Token, 
            resetPasswordDto.NewPassword
        );

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok("Password reset successfully!");
    }
    
    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailDto confirmEmailDto)
    {
        var user = await userManager.FindByIdAsync(confirmEmailDto.UserId);
        if (user is null)
            return BadRequest("User with this id does not exist.");

        var result = await userManager.ConfirmEmailAsync(user, confirmEmailDto.Token);
    
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok("Email confirmed successfully.");
    }
}