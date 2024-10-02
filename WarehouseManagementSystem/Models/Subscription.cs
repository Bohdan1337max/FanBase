using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseManagementSystem.Models;

[Table("subscription")]
public class Subscription
{
    [Column("subscription_id")] public int Id { get; set; }

    [Column("subscriber_id")] public int SubscriberId { get; set; }
    public User Subscriber { get; set; }

    [Column("creator_id")] public int CreatorId { get; set; }
    public User Creator { get; set; }

    [Column("subscription_tier_id")] public int SubscriptionTierId { get; set; }
    public SubscriptionTier Tier { get; set; }

    [Column("start_date")] public DateTime StartDate { get; set; }
    [Column("end_date")] public DateTime? EndDate { get; set; }
}