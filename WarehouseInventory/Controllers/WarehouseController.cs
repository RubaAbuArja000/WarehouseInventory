using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Core.DTOs;
using Warehouse.Shared.Services;

namespace WarehouseInventory.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class WarehouseController(IWarehouseService _service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] WarehouseDto dto)
    {
        await _service.AddAsync(dto);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateWarehouseDto dto)
    {
        if (id != dto.Id) return BadRequest();
        await _service.UpdateAsync(dto);
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var warehouse = await _service.GetByIdAsync(id);
        return warehouse == null ? NotFound() : Ok(warehouse);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var warehouses = await _service.GetAllAsync();
        return Ok(warehouses);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok();
    }
}