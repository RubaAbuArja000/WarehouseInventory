using AutoMapper;
using Warehouse.Core.DTOs;
using WarehouseEntity = Warehouse.Core.Entities.Warehouse;

namespace Warehouse.Application.MappingProfiles;

public class WarehouseProfile : Profile
{
    public WarehouseProfile()
    {
        CreateMap<WarehouseDto, WarehouseEntity>().ReverseMap();
        CreateMap<CreateWarehouseDto, WarehouseEntity>();
        CreateMap<UpdateWarehouseDto, WarehouseEntity>();
    }
}
