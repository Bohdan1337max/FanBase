using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WarehouseManagementSystem.Models;

[Table("user_role")]
public class UserRole
{
    [Column("user_role_id")]public int UserRoleId { get; set; }
    [Column("user_id")] public int UserId { get; set; }
    public User User { get; set; }
    [Column("role_id")]public int RoleId { get; set; }
    public Role Role { get; set; }
}