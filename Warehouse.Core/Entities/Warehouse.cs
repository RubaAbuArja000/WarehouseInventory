using System.ComponentModel.DataAnnotations;

namespace Warehouse.Core.Entities;

public class Warehouse
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } 

    [Required]
    public string Address { get; set; }

    [Required]
    public string City { get; set; }

    [Required]
    public string Country { get; set; }

    public ICollection<Item> Items { get; set; }
}
