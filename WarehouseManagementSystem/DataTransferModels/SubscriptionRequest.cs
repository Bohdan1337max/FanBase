namespace WarehouseManagementSystem.DataTransferModels;

public class SubscriptionRequest
{
    public int CreatorId { get; set; }
    public int SubscriptionTierId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}