using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseManagementSystem.Models;

[Table("subscription_tier")]
public class SubscriptionTier
{
    [Column("subscription_tier_id")]public int Id { get; set; }
    [Column("name")]public string Name { get; set; }
    [Column("price")]public decimal Price { get; set; }
    [Column("description")]public string? Description { get; set; }

    [Column("creator_id")]public int CreatorId { get; set; }
    public User Creator { get; set; }

    public ICollection<Subscription> Subscriptions { get; set; }
}