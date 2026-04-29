using Warehouse.Core.DTOs;
using Warehouse.Core.Entities;
using Warehouse.Core.Exceptions;
using Warehouse.Core.IRepositories;
using Warehouse.Shared.Services;

namespace Warehouse.Services;

public class WarehouseItemService(IWarehouseItemRepository _repository) : IWarehouseItemService
{
    public async Task<IEnumerable<WarehouseItemDto>> GetAllAsync()
    {
        var items = await _repository.GetAllAsync();
        return items.Select(ToDto);
    }

    public async Task<WarehouseItemDto?> GetByIdAsync(int id)
    {
        var item = await _repository.GetByIdAsync(id);
        return item is null ? null : ToDto(item);
    }

    public async Task AddAsync(CreateWarehouseItemDto dto)
    {
        await _repository.AddAsync(new WarehouseItem
        {
            ItemName = dto.ItemName,
            SkuCode = dto.SkuCode,
            Qty = dto.Quantity,
            CostPrice = dto.CostPrice,
            MsrpPrice = dto.MsrpPrice,
            WarehouseId = dto.WarehouseId
        });
    }

    public async Task UpdateAsync(int id, UpdateWarehouseItemDto dto)
    {
        var item = await _repository.GetByIdAsync(id)
            ?? throw new ResourceNotFoundException($"Warehouse item with ID {id} not found.");

        item.ItemName = dto.ItemName;
        item.SkuCode = dto.SkuCode;
        item.Qty = dto.Quantity;
        item.CostPrice = dto.CostPrice;
        item.MsrpPrice = dto.MsrpPrice;
        item.WarehouseId = dto.WarehouseId;

        await _repository.UpdateAsync(item);
    }

    public async Task DeleteAsync(int id)
    {
        var item = await _repository.GetByIdAsync(id)
            ?? throw new ResourceNotFoundException($"Warehouse item with ID {id} not found.");

        await _repository.DeleteAsync(item);
    }

    public async Task<IEnumerable<WarehouseItemDto>> GetTopHighItemsAsync(int count) =>
        await _repository.GetTopHighItemsAsync(count);

    public async Task<IEnumerable<WarehouseItemDto>> GetTopLowItemsAsync(int count) =>
        await _repository.GetTopLowItemsAsync(count);

    private static WarehouseItemDto ToDto(WarehouseItem i) => new()
    {
        Id = i.Id,
        ItemName = i.ItemName,
        SkuCode = i.SkuCode,
        Quantity = i.Qty,
        CostPrice = i.CostPrice,
        MsrpPrice = i.MsrpPrice,
        WarehouseId = i.WarehouseId,
        WarehouseName = i.Warehouse?.Name ?? string.Empty
    };
}
