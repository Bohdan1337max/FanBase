using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseManagementSystem.Models;

[Table("users")]
public class User
{
    [Column("user_id")]public int Id { get; set; }
    [Column("user_name")]public string? UserName { get; set; }
    [Column("email")]public string? Email { get; set; }
    [Column("password")]public string? Password { get; set; }
    [Column("salt")]public string? Salt { get; set; }
}