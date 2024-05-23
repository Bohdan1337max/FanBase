using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseManagementSystem.Models;

[Table("item")]
public class Item
{
    [Column("item_id")]public int Id { get; set; }
    [Column("created_date")]  public DateTime? CreatedDate { get; set; }
    [Column("name")]public string? Name { get; set; }
    [Column("description")]public string? Description { get; set; }
    
}