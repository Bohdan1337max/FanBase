using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseManagementSystem.Models;

[Table("inventory")]
public class Inventory
{
    [Column("inventory_id")]public int Id { get; set; }
    [Column("item_id")]public int ItemId { get; set; }
    public Item Item { get; set; }
    [Column("name")]public string? Name { get; set; }
    [Column("description")]public string? Description { get; set; }
    [Column("location")]public string? Location { get; set; }
    [Column("quantity")]public int Quantity { get; set; }
    [Column("reserved_quantity")]public int ReservedQuantity { get; set; }
}