namespace WarehouseManagementSystem.DataTransferModels;

public class SubscriptionTierResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
}