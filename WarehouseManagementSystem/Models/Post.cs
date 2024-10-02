using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseManagementSystem.Models;

[Table("post")]
public class Post
{
    [Column("post_id")]public int Id { get; set; }
    [Column("title")]public string Title { get; set; }
    [Column("description")]public string Description { get; set; }
    [Column("image_url")]public string? ImageUrl { get; set; }
    [Column("creation_time")]public DateTime CreationTime { get; set; }
    [Column("edit_time")]public DateTime? EditTime { get; set; }
    [Column("creator_id")]public int CreatorId { get; set; }
}