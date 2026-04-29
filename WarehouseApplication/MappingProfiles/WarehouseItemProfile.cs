using AutoMapper;
using Warehouse.Core.DTOs;
using Warehouse.Core.Entities;

namespace Warehouse.Application.MappingProfiles;

public class WarehouseItemProfile : Profile
{
    public WarehouseItemProfile()
    {
        CreateMap<WarehouseItem, WarehouseItemDto>().ReverseMap();
        CreateMap<CreateWarehouseItemDto, WarehouseItem>();
        CreateMap<UpdateWarehouseItemDto, WarehouseItem>();
    }
}
