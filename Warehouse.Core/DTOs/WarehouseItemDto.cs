namespace Warehouse.Core.DTOs;

public class WarehouseItemDto
{
    public int Id { get; set; }
    public string ItemName { get; set; } = default!;
    public string? SkuCode { get; set; }
    public int Quantity { get; set; }
    public decimal CostPrice { get; set; }
    public decimal? MsrpPrice { get; set; }
    public int WarehouseId { get; set; }
    public string WarehouseName { get; set; } = default!;
}
