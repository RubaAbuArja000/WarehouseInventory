using Warehouse.Core.DTOs;

namespace Warehouse.Shared.Services;

public interface IWarehouseItemService
{
    Task<IEnumerable<WarehouseItemDto>> GetAllAsync();
    Task<WarehouseItemDto?> GetByIdAsync(int id);
    Task AddAsync(CreateWarehouseItemDto dto);
    Task UpdateAsync(int id, UpdateWarehouseItemDto dto);
    Task DeleteAsync(int id);
    Task<IEnumerable<WarehouseItemDto>> GetTopHighItemsAsync(int count);
    Task<IEnumerable<WarehouseItemDto>> GetTopLowItemsAsync(int count);
}