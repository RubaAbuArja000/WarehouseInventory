using Microsoft.EntityFrameworkCore;
using Warehouse.Core.IRepositories;
using Warehouse.DataAccess.Persistence;
using WarehouseEntity = Warehouse.Core.Entities.Warehouse;

namespace Warehouse.DataAccess.Repositories;

public class WarehouseRepository(DatabaseContext _context) : IWarehouseRepository
{
    public async Task<List<WarehouseEntity>> GetAllAsync()
    {
        return await _context.Warehouses
            .Include(w => w.Items)
            .ToListAsync();
    }

    public async Task<WarehouseEntity?> GetByIdAsync(int id)
    {
        return await _context.Warehouses
            .Include(w => w.Items)
            .FirstOrDefaultAsync(w => w.Id == id);
    }

    public async Task AddAsync(WarehouseEntity warehouse)
    {
        await _context.Warehouses.AddAsync(warehouse);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(WarehouseEntity warehouse)
    {
        _context.Warehouses.Update(warehouse);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(WarehouseEntity warehouse)
    {
        _context.Warehouses.Remove(warehouse);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.Warehouses.AnyAsync(w => w.Name == name);
    }
}
