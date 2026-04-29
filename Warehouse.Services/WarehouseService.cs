using Warehouse.Application.Exceptions;
using Warehouse.Core.DTOs;
using Warehouse.Core.Exceptions;
using Warehouse.Core.IRepositories;
using Warehouse.Shared.Services;
using WarehouseEntity = Warehouse.Core.Entities.Warehouse;

namespace Warehouse.Services;

public class WarehouseService(IWarehouseRepository _repository) : IWarehouseService
{
    public async Task<List<WarehouseDto>> GetAllAsync()
    {
        var warehouses = await _repository.GetAllAsync();
        return warehouses.Select(ToDto).ToList();
    }

    public async Task<WarehouseDto?> GetByIdAsync(int id)
    {
        var warehouse = await _repository.GetByIdAsync(id);
        return warehouse is null ? null : ToDto(warehouse);
    }

    public async Task AddAsync(WarehouseDto dto)
    {
        if (await _repository.ExistsByNameAsync(dto.Name))
            throw new BadRequestException($"A warehouse named '{dto.Name}' already exists.");

        await _repository.AddAsync(new WarehouseEntity
        {
            Name = dto.Name,
            Address = dto.Address,
            City = dto.City,
            Country = dto.Country
        });
    }

    public async Task UpdateAsync(UpdateWarehouseDto dto)
    {
        var warehouse = await _repository.GetByIdAsync(dto.Id)
            ?? throw new ResourceNotFoundException($"Warehouse with ID {dto.Id} not found.");

        if (warehouse.Name != dto.Name && await _repository.ExistsByNameAsync(dto.Name))
            throw new BadRequestException($"A warehouse named '{dto.Name}' already exists.");

        warehouse.Name = dto.Name;
        warehouse.Address = dto.Address;
        warehouse.City = dto.City;
        warehouse.Country = dto.Country;

        await _repository.UpdateAsync(warehouse);
    }

    public async Task DeleteAsync(int id)
    {
        var warehouse = await _repository.GetByIdAsync(id)
            ?? throw new ResourceNotFoundException($"Warehouse with ID {id} not found.");

        await _repository.DeleteAsync(warehouse);
    }

    private static WarehouseDto ToDto(WarehouseEntity w) => new()
    {
        Id = w.Id,
        Name = w.Name,
        Address = w.Address,
        City = w.City,
        Country = w.Country
    };
}
