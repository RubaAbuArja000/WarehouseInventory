using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Core.Entities;
using Warehouse.Shared.Services;

namespace WarehouseInventory.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ItemController(IItemService _service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Item item)
    {
        await _service.AddAsync(item);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Item item)
    {
        if (id != item.Id) return BadRequest("Route id does not match item id.");
        await _service.UpdateAsync(item);
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var item = await _service.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpGet("warehouse/{warehouseId}")]
    public async Task<IActionResult> GetAll(int warehouseId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var items = await _service.GetAllAsync(warehouseId, page, pageSize);
        return Ok(items);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}