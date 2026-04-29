using Warehouse.Core.Entities;
using Warehouse.Core.Exceptions;
using Warehouse.Core.IRepositories;
using Warehouse.Shared.Services;

namespace Warehouse.Services;

public class ItemService(IItemRepository _repository) : IItemService
{
    public async Task<List<Item>> GetAllAsync(int warehouseId, int page = 1, int pageSize = 10) =>
        await _repository.GetAllAsync(warehouseId, page, pageSize);

    public async Task<Item?> GetByIdAsync(int id) =>
        await _repository.GetByIdAsync(id);

    public async Task AddAsync(Item item)
    {
        if (await _repository.ExistsByNameAsync(item.Name, item.WarehouseId))
            throw new BadRequestException($"An item named '{item.Name}' already exists in this warehouse.");

        await _repository.AddAsync(item);
    }

    public async Task UpdateAsync(Item item) =>
        await _repository.UpdateAsync(item);

    public async Task DeleteAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id)
            ?? throw new ResourceNotFoundException($"Item with ID {id} not found.");

        await _repository.DeleteAsync(entity);
    }
}
