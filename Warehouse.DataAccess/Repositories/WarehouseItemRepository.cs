using Microsoft.EntityFrameworkCore;
using Warehouse.Core.DTOs;
using Warehouse.Core.Entities;
using Warehouse.Core.IRepositories;
using Warehouse.DataAccess.Persistence;

namespace Warehouse.DataAccess.Repositories;

public class WarehouseItemRepository(DatabaseContext _context) : IWarehouseItemRepository
{
    public async Task<IEnumerable<WarehouseItem>> GetAllAsync()
    {
        return await _context.WarehouseItems
                             .Include(i => i.Warehouse)
                             .ToListAsync();
    }
    public async Task<WarehouseItem?> GetByIdAsync(int id)
    {
        return await _context.WarehouseItems
                             .Include(i => i.Warehouse)
                             .FirstOrDefaultAsync(i => i.Id == id);
    }
    public async Task AddAsync(WarehouseItem item)
    {
        await _context.WarehouseItems.AddAsync(item);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(WarehouseItem item)
    {
        _context.WarehouseItems.Update(item);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(WarehouseItem item)
    {
        _context.WarehouseItems.Remove(item);
        await _context.SaveChangesAsync();
    }
    public async Task<IEnumerable<WarehouseStatusDto>> GetWarehouseStatusAsync()
    {
        return await _context.Warehouses
            .Select(w => new WarehouseStatusDto
            {
                WarehouseId = w.Id,
                WarehouseName = w.Name,
                TotalItems = w.Items.Count()
            })
            .ToListAsync();
    }
    public async Task<IEnumerable<WarehouseItemDto>> GetTopHighItemsAsync(int count)
    {
        return await _context.WarehouseItems
            .OrderByDescending(i => i.Qty)
            .Take(count)
            .Select(i => new WarehouseItemDto
            {
                Id = i.Id,
                ItemName = i.ItemName,
                SkuCode = i.SkuCode,
                Quantity = i.Qty,
                CostPrice = i.CostPrice,
                MsrpPrice = i.MsrpPrice,
                WarehouseId = i.WarehouseId
            })
            .ToListAsync();
    }
    public async Task<IEnumerable<WarehouseItemDto>> GetTopLowItemsAsync(int count)
    {
        return await _context.WarehouseItems
            .OrderBy(i => i.Qty)
            .Take(count)
            .Select(i => new WarehouseItemDto
            {
                Id = i.Id,
                ItemName = i.ItemName,
                SkuCode = i.SkuCode,
                Quantity = i.Qty,
                CostPrice = i.CostPrice,
                MsrpPrice = i.MsrpPrice,
                WarehouseId = i.WarehouseId
            })
            .ToListAsync();
    }
}