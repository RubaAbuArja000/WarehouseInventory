using Warehouse.Core.DTOs;
using Warehouse.Core.IRepositories;
using Warehouse.Shared.Services;

namespace Warehouse.Services;

public class DashboardService(IWarehouseItemRepository _itemRepo, IWarehouseRepository _warehouseRepo) : IDashboardService
{
    public async Task<IEnumerable<WarehouseStatusDto>> GetWarehouseStatusAsync()
    {
        var warehouses = await _warehouseRepo.GetAllAsync();
        return warehouses.Select(w => new WarehouseStatusDto
        {
            WarehouseId = w.Id,
            WarehouseName = w.Name,
            TotalItems = w.Items?.Count ?? 0
        });
    }
    public async Task<IEnumerable<ItemDto>> GetTopHighItemsAsync(int count = 10)
    {
        var items = await _itemRepo.GetTopHighItemsAsync(count);
        return items.Select(i => new ItemDto { Id = i.Id, Name = i.ItemName, Quantity = i.Quantity });
    }
    public async Task<IEnumerable<ItemDto>> GetTopLowItemsAsync(int count = 10)
    {
        var items = await _itemRepo.GetTopLowItemsAsync(count);
        return items.Select(i => new ItemDto { Id = i.Id, Name = i.ItemName, Quantity = i.Quantity });
    }
}