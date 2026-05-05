using Warehouse.Core.DTOs;
namespace Warehouse.Shared.Services;

public interface IDashboardService
{
    Task<IEnumerable<WarehouseStatusDto>> GetWarehouseStatusAsync();
    Task<IEnumerable<ItemDto>> GetTopHighItemsAsync(int count = 10);
    Task<IEnumerable<ItemDto>> GetTopLowItemsAsync(int count = 10);
    Task<IEnumerable<ItemDto>> GetTopSellingItemsAsync(int count = 10);
    Task<IEnumerable<ItemDto>> GetLowStockItemsAsync(int count = 10);
    Task<IEnumerable<ItemDto>> GetOutOfStockItemsAsync(int count = 10);
}