using System.Security.Claims;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationApi.Dtos.UserDtos;
using WebApplicationApi.Entity;
using WebApplicationApi.Services;
using WebApplicationApi.Helpers; 

namespace WebApplicationApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController(
    IValidator<RegisterDto> registerValidator,
    UserManager<AppUser> userManager,
    RoleManager<IdentityRole> roleManager,
    IConfiguration config,
    JwtService jwtService,
    IMapper mapper
) : ControllerBase
{

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var validationResult = registerValidator.Validate(registerDto);
        if (!validationResult.IsValid)
        {
            var errorList = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return BadRequest(ResponseModelHelper.CreateErrorResponse<string>(errorList));
        }

        var user = await userManager.FindByNameAsync(registerDto.UserName);
        if (user is not null)
            return BadRequest(ResponseModelHelper.CreateErrorResponse<string>(new List<string> { "User with this username already exists" }));

        user = mapper.Map<AppUser>(registerDto);

        var result = await userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            var errorList = result.Errors.Select(e => e.Description).ToList();
            return BadRequest(ResponseModelHelper.CreateErrorResponse<string>(errorList));
        }

        await userManager.AddToRoleAsync(user, "Member");

        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        
        var responseData = new
        {
            message = "User created successfully. Please confirm your email.",
            userId = user.Id,
            token = token
        };

        return Ok(ResponseModelHelper.CreateSuccessResponse(responseData));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = await userManager.FindByNameAsync(loginDto.UserName);
        if (user is null)
            return BadRequest(ResponseModelHelper.CreateErrorResponse<string>(new List<string> { "Invalid username or password" }));

        var result = await userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!result)
            return BadRequest(ResponseModelHelper.CreateErrorResponse<string>(new List<string> { "Invalid username or password" }));

        if (!await userManager.IsEmailConfirmedAsync(user))
            return BadRequest(ResponseModelHelper.CreateErrorResponse<string>(new List<string> { "Please confirm your email before logging in." }));

        var roles = await userManager.GetRolesAsync(user);

        var accessToken = jwtService.GenerateToken(user, roles, config);
        var refreshToken = jwtService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpireTime = DateTime.Now.AddDays(7);
        await userManager.UpdateAsync(user);
        
        var responseData = new
        {
            accessToken = accessToken,
            refreshToken = refreshToken
        };

        return Ok(ResponseModelHelper.CreateSuccessResponse(responseData));
    }

    [HttpGet("profile")]
    [Authorize]
    public IActionResult Profile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userName = User.Identity?.Name ?? User.FindFirstValue(ClaimTypes.Name);
        var fullName = User.FindFirstValue("FullName");
        var roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();

        var responseData = new
        {
            userId,
            fullName,
            userName,
            roles
        };

        return Ok(ResponseModelHelper.CreateSuccessResponse(responseData));
    }

    [HttpGet("forgotpassword")]
    public async Task<IActionResult> ForgotPassword(string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is null)
            return BadRequest(ResponseModelHelper.CreateErrorResponse<string>(new List<string> { "User with this email does not exist." }));

        var token = await userManager.GeneratePasswordResetTokenAsync(user);

        var responseData = new { message = "Password reset token generated.", token = token };
        return Ok(ResponseModelHelper.CreateSuccessResponse(responseData));
    }

    [HttpPost("resetpassword")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
    {
        var user = await userManager.FindByEmailAsync(resetPasswordDto.Email);
        if (user is null)
            return BadRequest(ResponseModelHelper.CreateErrorResponse<string>(new List<string> { "User with this email does not exist." }));

        var result = await userManager.ResetPasswordAsync(
            user,
            resetPasswordDto.Token,
            resetPasswordDto.NewPassword
        );

        if (!result.Succeeded)
        {
            var errorList = result.Errors.Select(e => e.Description).ToList();
            return BadRequest(ResponseModelHelper.CreateErrorResponse<string>(errorList));
        }

        return Ok(ResponseModelHelper.CreateSuccessResponse("Password reset successfully!"));
    }

    [HttpGet("confirmemail")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user is null)
            return BadRequest(ResponseModelHelper.CreateErrorResponse<string>(new List<string> { "User with this id does not exist." }));

        var result = await userManager.ConfirmEmailAsync(user, token);

        if (!result.Succeeded)
        {
            var errorList = result.Errors.Select(e => e.Description).ToList();
            return BadRequest(ResponseModelHelper.CreateErrorResponse<string>(errorList));
        }

        return Ok(ResponseModelHelper.CreateSuccessResponse("Email confirmed successfully."));
    }

    [HttpPost("refreshtoken")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto dto)
    {
        var user = await userManager.Users
            .FirstOrDefaultAsync(u => u.RefreshToken == dto.RefreshToken);

        if (user is null || user.RefreshTokenExpireTime <= DateTime.Now)
            return BadRequest(ResponseModelHelper.CreateErrorResponse<string>(new List<string> { "Invalid or expired refresh token." }));

        var roles = await userManager.GetRolesAsync(user);

        var newAccessToken = jwtService.GenerateToken(user, roles, config);
        var newRefreshToken = jwtService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpireTime = DateTime.Now.AddDays(7);
        await userManager.UpdateAsync(user);

        var responseData = new
        {
            accessToken = newAccessToken,
            refreshToken = newRefreshToken
        };

        return Ok(ResponseModelHelper.CreateSuccessResponse(responseData));
    }
}