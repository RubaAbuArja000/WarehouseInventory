using Warehouse.Core.Entities;

namespace Warehouse.Shared.Services;

public interface IItemService
{
    Task<List<Item>> GetAllAsync(int warehouseId, int page = 1, int pageSize = 10);
    Task<Item?> GetByIdAsync(int id);
    Task AddAsync(Item item);
    Task UpdateAsync(Item item);
    Task DeleteAsync(int id);
}
