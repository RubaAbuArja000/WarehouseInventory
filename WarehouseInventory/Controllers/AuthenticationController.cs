using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Core.DTOs;
using Warehouse.Core.Entities;
using Warehouse.Shared.Services;

namespace WarehouseInventory.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthenticationController(IAuthService _authService, IUserService _userService) : ControllerBase
{
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginDto request)
    {
        try
        {
            var token = await _authService.LoginAsync(request.Email, request.Password);
            var expiresAt = DateTime.UtcNow.AddHours(1).ToString("o");
            return Ok(new { token, expiresAt });
        }
        catch (Exception ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] CreateUserRequest request)
    {
        var user = new User
        {
            Email = request.Email,
            FullName = request.FullName,
            Role = request.Role,
            IsActive = request.IsActive
        };

        await _userService.AddAsync(user, request.Password);
        return Ok();
    }
}