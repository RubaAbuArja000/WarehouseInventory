using Microsoft.EntityFrameworkCore;
using Warehouse.Core.Entities;
using Warehouse.Core.IRepositories;
using Warehouse.DataAccess.Persistence;

namespace Warehouse.DataAccess.Repositories;

public class ItemRepository(DatabaseContext _context) : IItemRepository
{
    public async Task<List<Item>> GetAllAsync(int warehouseId, int page = 1, int pageSize = 10)
    {
        return await _context.Items
            .Where(i => i.WarehouseId == warehouseId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
    public async Task<Item?> GetByIdAsync(int id)
    {
        return await _context.Items.FindAsync(id);
    }
    public async Task AddAsync(Item item)
    {
        await _context.Items.AddAsync(item);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(Item item)
    {
        _context.Items.Update(item);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(Item item)
    {
        _context.Items.Remove(item);
        await _context.SaveChangesAsync();
    }
    public async Task<bool> ExistsByNameAsync(string name, int warehouseId)
    {
        return await _context.Items
            .AnyAsync(i => i.Name == name && i.WarehouseId == warehouseId);
    }
}
