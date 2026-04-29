using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Core.Entities;
using Warehouse.Shared.Services;

namespace WarehouseInventory.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsersController(IUserService _userService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
    {
        var user = new User
        {
            Email = request.Email,
            FullName = request.FullName,
            Role = request.Role,
            IsActive = request.IsActive
        };
        await _userService.AddAsync(user, request.Password);
        return Ok(ToDto(user));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserRequest request)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null) return NotFound();
        user.FullName = request.FullName;
        user.Role = request.Role;
        user.IsActive = request.IsActive;
        await _userService.UpdateAsync(user);
        return Ok(ToDto(user));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(ToDto(user));
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users.Select(ToDto));
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> ToggleStatus(int id, [FromBody] ToggleStatusRequest request)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null) return NotFound();
        user.IsActive = request.IsActive;
        await _userService.UpdateAsync(user);
        return Ok(ToDto(user));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _userService.DeleteAsync(id);
        return NoContent();
    }

    private static object ToDto(User u) => new
    {
        id = u.Id,
        name = u.FullName,
        email = u.Email,
        role = u.Role,
        isActive = u.IsActive,
        createdAt = u.CreatedAt.ToString("o")
    };
}