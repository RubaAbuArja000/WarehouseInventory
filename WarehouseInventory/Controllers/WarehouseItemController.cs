using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Core.DTOs;
using Warehouse.Shared.Services;

namespace WarehouseInventory.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class WarehouseItemController(IWarehouseItemService _warehouseItemService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateWarehouseItemDto dto)
    {
        await _warehouseItemService.AddAsync(dto);
        return Ok(new { message = "Warehouse item created successfully." });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateWarehouseItemDto dto)
    {
        await _warehouseItemService.UpdateAsync(id, dto);
        return Ok(new { message = "Warehouse item updated successfully." });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _warehouseItemService.GetByIdAsync(id);
        if (item is null) return NotFound();
        return Ok(item);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _warehouseItemService.GetAllAsync();
        return Ok(items);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _warehouseItemService.DeleteAsync(id);
        return NoContent();
    }
}