using WarehouseEntity = Warehouse.Core.Entities.Warehouse;

namespace Warehouse.Core.IRepositories;

public interface IWarehouseRepository
{
    Task<List<WarehouseEntity>> GetAllAsync();
    Task<WarehouseEntity?> GetByIdAsync(int id);
    Task AddAsync(WarehouseEntity warehouse);
    Task UpdateAsync(WarehouseEntity warehouse);
    Task DeleteAsync(WarehouseEntity warehouse);
    Task<bool> ExistsByNameAsync(string name);
}
