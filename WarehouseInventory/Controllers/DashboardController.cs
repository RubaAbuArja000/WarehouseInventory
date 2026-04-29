using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Shared.Services;

namespace WarehouseInventory.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DashboardController(IDashboardService _dashboardService, IWarehouseItemService _warehouseItemService) : ControllerBase
{
    [HttpGet("warehouse-status")]
    public async Task<IActionResult> GetWarehouseStatus()
    {
        var result = await _dashboardService.GetWarehouseStatusAsync();
        return Ok(result);
    }

    [HttpGet("top-high-items")]
    public async Task<IActionResult> GetTopHighItems()
    {
        var result = await _dashboardService.GetTopHighItemsAsync();
        return Ok(result);
    }

    [HttpGet("top-low-items")]
    public async Task<IActionResult> GetTopLowItems()
    {
        var result = await _dashboardService.GetTopLowItemsAsync();
        return Ok(result);
    }
}