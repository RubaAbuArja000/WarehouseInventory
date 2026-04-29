using System.ComponentModel.DataAnnotations;

namespace Warehouse.Core.Entities;

public class Item
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } 

    public string? SkuCode { get; set; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [Required]
    public decimal CostPrice { get; set; }

    public decimal? MsrpPrice { get; set; }

    [Required]
    public int WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; }
}
