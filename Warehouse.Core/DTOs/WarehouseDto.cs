namespace Warehouse.Core.DTOs;

public class WarehouseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
}
public class CreateWarehouseDto
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
}
public class UpdateWarehouseDto
{
    public int Id { get; set; } 
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
}
