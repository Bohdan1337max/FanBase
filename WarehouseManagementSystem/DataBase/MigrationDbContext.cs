using Microsoft.EntityFrameworkCore;

namespace WarehouseManagementSystem.DataBase;

public class MigrationDbContext(DbContextOptions<MigrationDbContext> options) : DbContext(options)
{
    
    
}