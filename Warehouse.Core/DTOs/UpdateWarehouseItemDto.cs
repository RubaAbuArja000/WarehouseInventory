using System.ComponentModel.DataAnnotations;

namespace Warehouse.Core.DTOs;

public class UpdateWarehouseItemDto
{
    [Required]
    public string ItemName { get; set; } = default!;

    public string? SkuCode { get; set; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [Required]
    public decimal CostPrice { get; set; }

    public decimal? MsrpPrice { get; set; }

    [Required]
    public int WarehouseId { get; set; }
}
