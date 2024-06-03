using Microsoft.EntityFrameworkCore;
using WarehouseManagementSystem.Models;

namespace WarehouseManagementSystem.DataBase;

public class WmsDbContext(DbContextOptions<WmsDbContext> options) : DbContext(options)
{
    public DbSet<Item> Items { get; set; }
    public DbSet<User> Users { get; set; }

    public DbSet<Inventory> Inventories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(
            action: Console.WriteLine,
            minimumLevel: LogLevel.Information);
    }
}