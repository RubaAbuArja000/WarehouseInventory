using Warehouse.Core.DTOs;

namespace Warehouse.Shared.Services;

public interface IWarehouseService
{
    Task<List<WarehouseDto>> GetAllAsync();
    Task<WarehouseDto?> GetByIdAsync(int id);
    Task AddAsync(WarehouseDto dto);
    Task UpdateAsync(UpdateWarehouseDto dto);
    Task DeleteAsync(int id);
}
