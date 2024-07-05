using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseManagementSystem.Models;

[Table("role")]
public class Role
{
    [Column("role_id")] public int Id { get; set; }
    [Column("name")]public string Name { get; set; } = null!;

    public ICollection<UserRole> UserRoles { get; set; }
}