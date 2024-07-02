namespace WarehouseManagementSystem.Models;

public class ItemCreateRequest
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    
    public string DefaultInventoryName { get; set; } = null!;

    public string? DefaultLocation { get; set; }

    public int Quantity { get; set; }
    
}