using Microsoft.EntityFrameworkCore;

namespace WarehouseManagementSystem.DataBase;

public class WmsDbContext : DbContext
{
    public DbSet<Item> Items { get; set; }

    public WmsDbContext(DbContextOptions<WmsDbContext> options) : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(
            action: Console.WriteLine,
            minimumLevel: LogLevel.Information);
    }
}