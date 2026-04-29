using Warehouse.Core.Entities;

namespace Warehouse.Core.IRepositories;

public interface IItemRepository
{
    Task<List<Item>> GetAllAsync(int warehouseId, int page = 1, int pageSize = 10);
    Task<Item?> GetByIdAsync(int id);
    Task AddAsync(Item item);
    Task UpdateAsync(Item item);
    Task DeleteAsync(Item item);
    Task<bool> ExistsByNameAsync(string name, int warehouseId);
}
