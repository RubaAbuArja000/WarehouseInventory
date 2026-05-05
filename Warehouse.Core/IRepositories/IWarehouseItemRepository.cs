using Warehouse.Core.DTOs;
using Warehouse.Core.Entities;

namespace Warehouse.Core.IRepositories;

public interface IWarehouseItemRepository
{
    Task<IEnumerable<WarehouseItem>> GetAllAsync();
    Task<WarehouseItem?> GetByIdAsync(int id);
    Task AddAsync(WarehouseItem item);
    Task UpdateAsync(WarehouseItem item);
    Task DeleteAsync(WarehouseItem item);
    Task<IEnumerable<WarehouseItemDto>> GetTopHighItemsAsync(int count);
    Task<IEnumerable<WarehouseItemDto>> GetTopLowItemsAsync(int count);
    Task<IEnumerable<WarehouseItemDto>> GetTopSellingItemsAsync(int count);
    Task<IEnumerable<WarehouseItemDto>> GetLowStockItemsAsync(int count);
    Task<IEnumerable<WarehouseItemDto>> GetOutOfStockItemsAsync(int count);
}
