namespace Warehouse.Core.DTOs;

public class ItemDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Quantity { get; set; }
}
