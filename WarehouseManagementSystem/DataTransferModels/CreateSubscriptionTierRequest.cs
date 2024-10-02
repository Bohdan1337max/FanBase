namespace WarehouseManagementSystem.DataTransferModels;

public class CreateSubscriptionTierRequest
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public int CreatorId { get; set; }
}